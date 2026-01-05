namespace MauiApp1;

public partial class HelloWorldPage : ContentPage
{
    public HelloWorldPage()
    {
        // Creazione del layout principale (vertical stack layout)
        var verticalStack = new VerticalStackLayout
        {
            VerticalOptions = LayoutOptions.Center,
            HorizontalOptions = LayoutOptions.Center,
            Spacing = 20
        };

        // Creazione del pulsante
        var button = new Button
        {
            Text = "Clicca qui",
            FontSize = 20,
            HorizontalOptions = LayoutOptions.Center,
            VerticalOptions = LayoutOptions.Center
        };

        // Gestione dell'evento Clicked del pulsante
        button.Clicked += async (sender, args) =>
        {
            await DisplayAlertAsync("Messaggio", "Hello World!", "OK");
        };

        // Aggiunta del pulsante al layout
        verticalStack.Children.Add(button);

        // Impostazione del layout come contenuto della pagina
        Content = verticalStack;
    }
}
