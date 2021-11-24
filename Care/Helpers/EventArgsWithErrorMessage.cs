using System;

namespace Care.Helpers {
    public class EventArgsWithErrorMessage : EventArgs {
        public string errorMessage {get; set;}

        public EventArgsWithErrorMessage (string errorMessage) {
            this.errorMessage = errorMessage;
        }
    }
} 