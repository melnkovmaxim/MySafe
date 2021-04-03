using Plugin.Printing;

namespace MySafe.Presentation
{
    public class PrintMessages : IBasePrintingMessages
    {
        public string AbandonedFormatString => nameof(AbandonedFormatString);
        public string AwaitingResponse => "Ожидание ответа";
        public string CanceledFormatString => nameof(CanceledFormatString);
        public string DefaultFormatString => nameof(DefaultFormatString);
        public string FailedFormatString => nameof(FailedFormatString);
        public string FailedToRegisterForPrinting => "Ошибка при регистрации на печать";
        public string NullDescriptionString => nameof(NullDescriptionString);
        public string PagesNotPresentErrorString => nameof(PagesNotPresentErrorString);
        public string PagesNotPresentWarningText => nameof(PagesNotPresentWarningText);
        public string PrinterServiceBusy => "Принтер занят";
        public string PrintInteractionCompleted => "Печать завершена";
        public string PrintInteractionError => "Ошибка при печати";
        public string SubmittedFormatString => nameof(SubmittedFormatString);
        public string UnableToPrintAtThisTime => nameof(UnableToPrintAtThisTime);
    }
}