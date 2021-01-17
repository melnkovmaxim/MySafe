using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;
using MySafe.Mediator.SignIn;
using Prism.Commands;

namespace MySafe.ViewModels
{
    public class DocumentViewModel
    {
        private readonly IMediator _mediator;

        public DocumentViewModel(IMediator mediator)
        {
            _mediator = mediator;
        }

        //public DelegateCommand LoadedCommand => new DelegateCommand(async () =>
        //    {
        //        var result = await _mediator.Send(new SignInCommand("", "password"));
        //    });
    }
}
