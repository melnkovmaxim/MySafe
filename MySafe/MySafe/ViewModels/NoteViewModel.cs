﻿using System.Collections.ObjectModel;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using MySafe.Core.Commands;
using MySafe.Core.Entities.Abstractions;
using MySafe.Core.Interfaces.Services;
using MySafe.Core.Models.Responses;
using MySafe.Presentation.Models;
using MySafe.Presentation.Models.Abstractions;
using MySafe.Presentation.PopupViews;
using MySafe.Presentation.PopupViews.Note;
using MySafe.Presentation.ViewModels.Abstractions;
using MySafe.Presentation.Views;
using MySafe.Services.Mediator.Notes.NoteListQuery;
using MySafe.Services.Mediator.Notes.RemoveNoteCommand;
using Prism.Navigation;
using Rg.Plugins.Popup.Services;
using Xamarin.Forms;

namespace MySafe.Presentation.ViewModels
{
    public class NoteViewModel : AuthorizedRefreshViewModel<EntityList<NoteEntity>, PresentationModelList<Note>>
    {
        private readonly IMediator _mediator;

        public AsyncCommand<Note> MoveToNoteEditCommand { get; }
        public AsyncCommand<Note> ShowToolMenuCommand { get; }
        public AsyncCommand AddNoteCommand { get; }

        public NoteViewModel(INavigationService navigationService, IMediator mediator, IMapper mapper, IAuthService authService) : base(
            navigationService, mapper, authService)
        {
            _mediator = mediator;

            ShowToolMenuCommand = new AsyncCommand<Note>(ShowToolMenuCommandTask);
            AddNoteCommand = new AsyncCommand(AddNoteCommandTask);
            MoveToNoteEditCommand = new AsyncCommand<Note>(MoveToNoteEditCommandTask);
            Notes = new ObservableCollection<Note>();
        }

        private async Task AddNoteCommandTask()
        {
            await _navigationService.NavigateAsync(nameof(NoteEditPage));
        }

        private async Task ShowToolMenuCommandTask(Note note)
        {
            const string removeOption = "Удалить";

            var result = await Application.Current.MainPage
                .DisplayActionSheet("", "Отмена", null, removeOption);

            if (result.Contains(removeOption))
            {
                _ = await _mediator.Send(new RemoveNoteCommand(note.Id));
            }
        }

        public ObservableCollection<Note> Notes { get; set; }

        public Task MoveToNoteEditCommandTask(Note note)
        {
            var @params = new NavigationParameters() { { nameof(NavigationParameter), new NavigationParameter(note.Id, note.ClippedContent) }};
            return _navigationService.NavigateAsync(nameof(NoteEditPage), @params);
        }

        protected override Task<EntityList<NoteEntity>> _refreshTask => _mediator.Send(new NoteListQuery());

        protected override void RefillObservableCollection(PresentationModelList<Note> noteList)
        {
            Notes.Clear();
            noteList.ForEach(Notes.Add);
        }
    }
}