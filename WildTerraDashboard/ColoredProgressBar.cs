using System;
using System.Drawing;
using System.Windows.Forms;

namespace WildTerraDashboard
{
    public class ColoredProgressBar : Control
    {
        // Propriedades
        public int Value { get; set; } = 100;
        public int Maximum { get; set; } = 100;
        public Color BarColor { get; set; } = Color.Red; // Cor padrão
        public Color BackgroundColor { get; set; } = Color.FromArgb(60, 60, 60); // Fundo cinza escuro

        public ColoredProgressBar()
        {
            this.DoubleBuffered = true; // Essencial para não piscar
            this.Size = new Size(200, 30); // Tamanho padrão
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            Graphics g = e.Graphics;

            // 1. Desenha o Fundo
            using (SolidBrush bgBrush = new SolidBrush(BackgroundColor))
            {
                g.FillRectangle(bgBrush, 0, 0, this.Width, this.Height);
            }

            // 2. Calcula a largura da barra colorida
            // Evita divisão por zero
            if (Maximum <= 0) Maximum = 100;
            // Proteção se o HP vier maior que o Máximo (bug do jogo ou buff)
            if (Value > Maximum) Maximum = Value;

            int width = (int)((float)Value / Maximum * this.Width);

            // 3. Desenha a Barra Colorida
            using (SolidBrush barBrush = new SolidBrush(BarColor))
            {
                g.FillRectangle(barBrush, 0, 0, width, this.Height);
            }

            // 4. Desenha o Texto (Ex: "85 / 100") centralizado
            string texto = $"{Value} / {Maximum}";
            using (Font f = new Font("Arial", 10, FontStyle.Bold))
            {
                SizeF len = g.MeasureString(texto, f);
                // Calcula o centro
                Point location = new Point(
                    (int)((this.Width / 2) - (len.Width / 2)),
                    (int)((this.Height / 2) - (len.Height / 2))
                );

                // Desenha sombra do texto (pra ler melhor)
                g.DrawString(texto, f, Brushes.Black, location.X + 1, location.Y + 1);
                // Desenha texto branco
                g.DrawString(texto, f, Brushes.White, location);
            }
        }
    }
}