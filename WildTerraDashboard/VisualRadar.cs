using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using System.Drawing.Drawing2D;

namespace WildTerraDashboard
{
    // Componente visual que desenha o sonar
    public class VisualRadar : PictureBox
    {
        private List<RadarEntity> entidades = new List<RadarEntity>();
        private float escala = 3.0f; // Zoom do radar

        public VisualRadar()
        {
            this.DoubleBuffered = true; // Evita piscar
            this.BackColor = Color.Black;
            this.Size = new Size(200, 200);
        }

        public void AtualizarEntidades(List<RadarEntity> novasEntidades)
        {
            this.entidades = novasEntidades;
            this.Invalidate(); // Pede para redesenhar
        }

        protected override void OnPaint(PaintEventArgs pe)
        {
            base.OnPaint(pe);
            Graphics g = pe.Graphics;
            g.SmoothingMode = SmoothingMode.AntiAlias;

            int cx = this.Width / 2;
            int cy = this.Height / 2;

            // Desenha Mira Verde
            Pen penVerde = new Pen(Color.FromArgb(50, 255, 50), 1);
            g.DrawLine(penVerde, cx, 0, cx, this.Height);
            g.DrawLine(penVerde, 0, cy, this.Width, cy);
            g.DrawEllipse(penVerde, cx - 30, cy - 30, 60, 60); // Círculo 10m
            g.DrawEllipse(penVerde, cx - 90, cy - 90, 180, 180); // Círculo 30m

            // Desenha Jogador (Centro)
            g.FillEllipse(Brushes.White, cx - 2, cy - 2, 5, 5);

            // Desenha Pontos
            foreach (var ent in entidades)
            {
                // Converte Coordenada Relativa -> Pixel na Tela
                float screenX = cx + (ent.RelX * escala);
                float screenY = cy - (ent.RelY * escala); // Y invertido

                // Só desenha se estiver dentro do quadrado
                if (screenX > 0 && screenX < this.Width && screenY > 0 && screenY < this.Height)
                {
                    using (SolidBrush b = new SolidBrush(ent.Cor))
                    {
                        g.FillEllipse(b, screenX - 3, screenY - 3, 6, 6);
                    }
                }
            }
        }
    }
}