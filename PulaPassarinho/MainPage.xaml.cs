﻿namespace PulaPassarinho;

public partial class MainPage : ContentPage
{
	const int gravidade = 40; //pixel
	const int TempoEntreFrames = 25; //ms
	bool Morreu = false;
	public MainPage()
	{
		InitializeComponent();
	}
	void AplicarGravidade ()
	{
		alexandrepato.TranslationY += gravidade;
	}
	async Task Desenhar ()
	{
		while (!Morreu)
		{
			AplicarGravidade (); 
			await Task.Delay (TempoEntreFrames);
		}
	}
	protected override void OnAppearing ()
	{
		base.OnAppearing ();
		Desenhar();
	}
}

