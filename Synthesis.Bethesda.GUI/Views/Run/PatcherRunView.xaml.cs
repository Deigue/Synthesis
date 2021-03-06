using Noggog.WPF;
using ReactiveUI;
using System.Reactive.Disposables;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Reactive.Linq;

namespace Synthesis.Bethesda.GUI.Views
{
    public class PatcherRunViewBase : NoggogUserControl<PatcherRunVM> { }

    /// <summary>
    /// Interaction logic for PatcherRunView.xaml
    /// </summary>
    public partial class PatcherRunView : PatcherRunViewBase
    {
        public PatcherRunView()
        {
            InitializeComponent();
            this.WhenActivated(disposable =>
            {
                this.WhenAnyValue(x => x.ViewModel!.Config.DisplayName)
                    .BindToStrict(this, x => x.PatcherDetailName.Text)
                    .DisposeWith(disposable);
                this.WhenAnyValue(x => x.ViewModel!.Config)
                    .BindToStrict(this, x => x.PatcherIconDisplay.DataContext)
                    .DisposeWith(disposable);

                // Set state subheader
                this.WhenAnyValue(x => x.ViewModel!.State.Value)
                    .Select(state =>
                    {
                        return state switch
                        {
                            RunState.NotStarted => "Not Run",
                            RunState.Error => "Errored",
                            RunState.Finished => "Completed",
                            RunState.Started => "Running",
                            _ => throw new NotImplementedException()
                        };
                    })
                    .BindToStrict(this, x => x.StatusBlock.Text)
                    .DisposeWith(disposable);

                // Set up text output
                this.WhenAnyValue(x => x.ViewModel!.OutputLineDisplay)
                    .BindToStrict(this, x => x.OutputBox.ItemsSource)
                    .DisposeWith(disposable);
                this.WhenAnyValue(x => x.ViewModel!.OutputLineDisplay.Count)
                    .Select(count => count > 0 ? Visibility.Visible : Visibility.Hidden)
                    .BindToStrict(this, x => x.OutputBox.Visibility)
                    .DisposeWith(disposable);
            });
        }
    }
}
