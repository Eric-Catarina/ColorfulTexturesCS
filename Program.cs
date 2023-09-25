using System;
using System.IO;
using System.Numerics;
public class TexturasCores
{
    static string filename = "TexturaProcedural";
    public static void Main()
    {
        /*Console.WriteLine("Informe as dimensões da imagem.");
        Console.WriteLine("Largura: ");
        Config.WIDTH = int.Parse(Console.ReadLine());
        Console.WriteLine("Altura: ");
        Config.HEIGHT = int.Parse(Console.ReadLine());
        */
        Buffer buffer = new Buffer();


        // buffer.Clear(0);
        // filename = "DegradeLinhaRed";
        // buffer.DegradeLinha(0);
        // buffer.Save(filename);

        // buffer.Clear(0);
        // filename = "DegradeLinhaGreen";
        // buffer.DegradeLinha(1);
        // buffer.Save(filename);

        // buffer.Clear(0);
        // filename = "DegradeLinhaBlue";
        // buffer.DegradeLinha(2);
        // buffer.Save(filename);

        buffer.Clear(0);
        filename = "DegradeFaixas";
        buffer.DegradeFaixas();
        buffer.Save(filename);

    }
}
class Config
{
    public static int WIDTH = 3 * 200;
    public static int HEIGHT = 200;
    public static int MAXCOLORS = 255;
}
//** SALVAR IMAGEM
class SaveImage
{
    public static void Save(string s, string name)
    {
        string str = "P3\n" + Config.WIDTH / 3 + " " + Config.HEIGHT + "\n" +
        Config.MAXCOLORS + "\n";
        str += "#nome\n" + s;
        File.WriteAllText(name + ".ppm", str);
    }
}
public class Buffer
{ //** FRAME BUFFER
    public int[,] frame;
    public Buffer()
    {
        frame = new int[Config.WIDTH, Config.HEIGHT];
    }
    public void Clear(int color)
    {
        for (int h = 0; h < Config.HEIGHT; h++)
        {
            for (int w = 0; w < Config.WIDTH; w++)
            {
                frame[w, h] = color;
            }
        }
    }
    public void SetPixel(int x, int y, int color)
    {
        x = Clamp(x, 0, Config.WIDTH - 1);
        y = Clamp(y, 0, Config.HEIGHT - 1);
        color = Clamp(color, 0, Config.MAXCOLORS);
        frame[x, y] = color;
    }
    int Clamp(int v, int min, int max)
    {
        return (v < min) ? min : (v > max) ? max : v;
    }
    public override string ToString()
    {
        string s = "";
        //for (int h = Config.HEIGHT-1; h >= 0; h--){
        for (int h = 0; h < Config.HEIGHT; h++)
        {
            for (int w = 0; w < Config.WIDTH; w++)
            {
                s += frame[w, h] + " ";
            }
            s += "\n";
        }
        return s;
    }
    public void Save(string name)
    {
        SaveImage.Save(ToString(), name);
    }
    public void DegradeLinha(int coord)
    {
        for (int h = 0; h < Config.HEIGHT; h++)
        {
            for (int w = 0; w < Config.WIDTH / 3; w++)
            {
                int cor = (int)(((float)3 * w / (float)Config.WIDTH) * (float)
                Config.MAXCOLORS);
                //Red
                SetPixel(3 * w + coord, h, cor);



            }
        }
    }
    public void DegradeFaixas()
    {
        Random rnd = new Random();
        Vector3 cor2 = new Vector3(Config.MAXCOLORS, Config.MAXCOLORS, Config.MAXCOLORS);
        for (int h = 0; h < Config.HEIGHT; h++)
        {
            Vector3 cor1 = new Vector3(0, 0, 0);
            float fator = (float)rnd.NextDouble();
            while (fator > 0.01) fator = (float)rnd.NextDouble();
            for (int w = 0; w < Config.WIDTH / 3; w++)
            {
                int cor = (int)(((float)3 * w / (float)Config.WIDTH) * (float)Config.MAXCOLORS);
                Vector3 seq = Vector3.Lerp(cor1, cor2, fator);
                cor1 = seq;
                SetPixel(3 * w, h, cor);
                cor = (int)cor1.X;
                SetPixel(3 * w + 1, h, cor);
                SetPixel(3 * w + 2, h, cor);
            }
        }
    }
}