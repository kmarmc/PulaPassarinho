namespace PulaPassarinho;

public partial class MainPage : ContentPage
{
	const int gravidade = 40; //pixel
	const int TempoEntreFrames = 25; //ms
	bool Morreu = false;
	double LarguraJanela=0;
	double AlturaJanela=0;
	int Velocidade=10;
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
			GerenciarCanos ();
			if (VerificarColisao())
			{
				Morreu = true;
				FrameGameOver.IsVisible = true;
				break;
			}
			await Task.Delay (TempoEntreFrames);
		}
	}
  protected override void OnSizeAllocated(double w, double h)
    {
        base.OnSizeAllocated(w, h);
		LarguraJanela= w;
		AlturaJanela= h;
    }
	void GerenciarCanos()
	{
		germancano.TranslationX -= Velocidade;
		canopvc.TranslationX -= Velocidade;
		if(canopvc.TranslationX <-LarguraJanela)
		{
			canopvc.TranslationX =0;
			germancano.TranslationX =0;
		}

	}
	void GameOver (object s, TappedEventArgs a)
	{
		FrameGameOver.IsVisible =false; 
		Inicializar();
		Desenhar();
	}
	bool VerificarColisao()
	{
		if (!Morreu)
		{
			if (VerificarColisaoTeto()||
			VerificarColisaoChao())
		{
		return true;
		}
		}
		return false;
	}
	bool VerificarColisaoTeto()
	{
		var minY=-AlturaJanela/2;
		if (alexandrepato.TranslationY <= minY)
			return true;
	    else
		    return false;
	}
	bool VerificarColisaoChao()
	{
		var maxY=-AlturaJanela/2 -gramado.HeightRequest;
		if (alexandrepato.TranslationY >= maxY)
			return true;
		else
			return false;	
	}
	void Inicializar()
	{
		Morreu= false;
		alexandrepato.TranslationY= 0;
	}

}

