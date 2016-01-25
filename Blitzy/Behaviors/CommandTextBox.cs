using System.Diagnostics.CodeAnalysis;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Interactivity;
using Blitzy.ViewModels.Main;

namespace Blitzy.Behaviors
{
	[ExcludeFromCodeCoverage]
	internal class CommandTextBox : Behavior<TextBox>

	{
		public CommandTextBox()
		{
		}

		internal bool OnPreviewKeyDown( Key key )
		{
			switch( key )
			{
			case Key.Tab:
				AutoComplete();
				break;

			case Key.Down:
				SelectNextCommand();
				break;

			case Key.Up:
				SelectPrevCommand();
				break;

			case Key.Return:
				ExecuteCommand( !( Keyboard.IsKeyDown( Key.LeftCtrl ) || Keyboard.IsKeyDown( Key.RightCtrl ) ) );
				break;

			default:
				return false;
			}

			return true;
		}

		[ExcludeFromCodeCoverage]
		protected override void OnAttached()
		{
			base.OnAttached();

			AssociatedObject.PreviewKeyDown += ( s, e ) => { e.Handled = OnPreviewKeyDown( e.Key ); };
		}

		private void AutoComplete()
		{
			if( InputProcessor == null )
			{
				return;
			}

			AssociatedObject.Text = InputProcessor.AutoCompleteInput( AssociatedObject.Text,
				Controller.CurrentCommand?.Command );
			AssociatedObject.CaretIndex = AssociatedObject.Text.Length;
		}

		private async void ExecuteCommand( bool primary )
		{
			await Controller.ExecuteCommand( primary, AssociatedObject.Text );
		}

		private void SelectNextCommand()
		{
			Controller.CurrentCommandIndex++;
		}

		private void SelectPrevCommand()
		{
			Controller.CurrentCommandIndex--;
		}

		public ICommandController Controller
		{
			get { return (ICommandController)GetValue( ControllerProperty ); }
			set { SetValue( ControllerProperty, value ); }
		}

		public IInputProcessor InputProcessor
		{
			get { return (IInputProcessor)GetValue( InputProcessorProperty ); }
			set { SetValue( InputProcessorProperty, value ); }
		}

		public static readonly DependencyProperty ControllerProperty = DependencyProperty.Register( "Controller",
					typeof( ICommandController ), typeof( CommandTextBox ), new PropertyMetadata( null ) );

		public static readonly DependencyProperty InputProcessorProperty = DependencyProperty.Register( "InputProcessor", typeof( IInputProcessor ), typeof( CommandTextBox ), new PropertyMetadata( null ) );
	}
}