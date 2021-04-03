using AutoMapper;
using MySafe.Core.Models.Responses;
using MySafe.Presentation.Models;

namespace MySafe.Presentation.EntityExtensions
{
    public static class NoteEntityExtensions
    {
        public static Note ToNotePresentationModel(this NoteEntity noteEntity)
        {
            return Ioc.Resolve<IMapper>().Map<Note>(noteEntity);
        }
    }
}