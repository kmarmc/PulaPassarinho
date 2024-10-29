using System.Runtime.InteropServices;

namespace PulaPassarinho;

public partial class MainPage : ContentPage
{
	const int gravidade = 20; //pixel
	const int TempoEntreFrames = 70; //ms
	bool Morreu = false;
	double LarguraJanela=0;
	double AlturaJanela=0;
	int Velocidade=10;
	const int maxTempoPulando = 2;
	int TempoPulando = 0;
	bool EstaPulando = false;
	const int ForcaPulo = 40;
	const int AberturaMinima = 200;
	int Score = 0;

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
			if (EstaPulando)
			  AplicaPulo();
			else
			AplicarGravidade (); 
			GerenciarCanos ();
			if (VerificarColisao())
			 SoundHelper.Play("morto.wav");

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
			var alturaMax = -100;
			var alturaMin = -canopvc.HeightRequest;
			germancano.TranslationY = Random.Shared.Next ((int)alturaMin, (int)alturaMax);
			canopvc.TranslationY = germancano.TranslationY+AberturaMinima+canopvc.HeightRequest;
			Score ++;
			LabelScore.Text = "Canos" + Score.ToString("D3");
		}

	}
	void GameOverClicked (object s, TappedEventArgs a)
	{
		FrameGameOver.IsVisible =false; 
		Inicializar();
		Desenhar();
	}
		void Inicializar()
	{
		germancano.TranslationX = -LarguraJanela;
		canopvc.TranslationX = -LarguraJanela;
		alexandrepato.TranslationX = 0;
		alexandrepato.TranslationY = 0;
		Morreu= false;
		alexandrepato.TranslationY= 0;
		Score= 0;
		GerenciarCanos();
	}


	bool VerificarColisao()
	{
		if (!Morreu)
		{
			if (VerificarColisaoTeto()||
			VerificarColisaoChao()||
			VerificarColisaoGermanCano()||
			VerificarColisaoCanoPvc())
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
		var maxY=AlturaJanela/2 -gramado.HeightRequest;
		if (alexandrepato.TranslationY >= maxY)
			return true;
		else
			return false;	
	}
	void AplicaPulo()
	{
		alexandrepato.TranslationY -= ForcaPulo;
		TempoPulando ++;
		if (TempoPulando >= maxTempoPulando)
		{
			EstaPulando = false;
			TempoPulando = 0;
		}
	}
	bool VerificarColisaoGermanCano()
	{
		var posHpato = (LarguraJanela/2) - (alexandrepato.WidthRequest/2);
		var posVpato = (LarguraJanela/2) - (alexandrepato.HeightRequest/2) + alexandrepato.TranslationY;
		if (posHpato >= Math.Abs (germancano.TranslationX) - germancano.WidthRequest &&
		posHpato <= Math.Abs (germancano.TranslationX) + germancano.WidthRequest &&
		posVpato <= germancano.HeightRequest + germancano.TranslationY)
		{
			return true;
		}
		else
		{
			return false;
		}
	}
	bool VerificarColisaoCanoPvc()
	{
		var posHpato = (LarguraJanela/2) - (alexandrepato.WidthRequest/2);
		var posVpato = (LarguraJanela/2) - (alexandrepato.HeightRequest/2) + alexandrepato.TranslationY;
		if (posHpato >= Math.Abs (canopvc.TranslationX) - canopvc.WidthRequest &&
		posHpato <= Math.Abs (canopvc.TranslationX) + canopvc.WidthRequest &&
		posVpato <= canopvc.HeightRequest + germancano.TranslationY)
		{
			return true;
		}
		else
		{
			return false;
		}

		
	}
	void GridClicked (object s, TappedEventArgs a)
	{
		EstaPulando = true;
	}

}

