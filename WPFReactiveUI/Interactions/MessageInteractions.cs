using ReactiveUI;
using System.Reactive;

namespace WPFReactiveUI.Interactions
{
    public static class MessageInteractions
    {
        public static Interaction<string, Unit> ShowMessage { get; } = new Interaction<string, Unit>();
    }
}
