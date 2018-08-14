namespace ExpenseTracker.Models.DataBases
{
    internal sealed class DataItemEventArgs<T>
    {
        private readonly ChangedAction changedAction;
        private readonly T item;

        public DataItemEventArgs(T item, ChangedAction changedAction)
        {
            this.item = item;
            this.changedAction = changedAction;
        }

        public ChangedAction ChangedAction => changedAction;

        public T DataItem => item;
    }
}