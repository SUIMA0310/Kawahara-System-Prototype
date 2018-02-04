using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using SharpDX;
using SharpDX.Direct2D1;
using SharpDX.DXGI;

using AlphaMode = SharpDX.Direct2D1.AlphaMode;
using Bitmap = SharpDX.Direct2D1.Bitmap;
using PixelFormat = SharpDX.Direct2D1.PixelFormat;
using SharpDX.Mathematics.Interop;
using Microsoft.AspNet.SignalR.Client;
using System.Windows;
using WindowsClientApplication.Models;
using System.Drawing.Imaging;
using System.Runtime.InteropServices;

namespace WindowsClientApplication.Controls {

    public class Reaction : D2dControl.D2dControl {

        public IReactionNotification ReactionNotification {
            get { return (IReactionNotification)GetValue( ReactionNotificationProperty ); }
            set { SetValue( ReactionNotificationProperty, value ); }
        }

        // Using a DependencyProperty as the backing store for ReactionNotification.  
        // This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ReactionNotificationProperty = DependencyProperty.Register( 
                "ReactionNotification", 
                typeof( IReactionNotification ), 
                typeof( Reaction ), 
                new PropertyMetadata( (s, e) => {

                    if ( s is Reaction sender ) {

                        if ( e.OldValue is IReactionNotification oldValue ) {
                            oldValue.ReactionReceived -= sender.OnReactionReceived;
                        }

                        if ( e.NewValue is IReactionNotification newValue ) {
                            newValue.ReactionReceived += sender.OnReactionReceived;
                        }

                    }

                } ) );

        private Queue<ReactionInfo> Queue { get; } = new Queue<ReactionInfo>();



        public Reaction(  ) {

            resCache.Add( "Bitmap", t => GetBitmap( t ) );

        }

        public override void Render(RenderTarget target) {

            target.Clear( new RawColor4( 1.0f, 1.0f, 1.0f, 0.0f ) );

            lock ( this.Queue ) {

                foreach ( var item in Queue ) {

                    var raw = new RawRectangleF();
                    var ret = GetPosition( item );
                    raw.Left = (float)ActualWidth / 2.0f + ret.x;
                    raw.Top = (float)ActualHeight * 0.9f - ret.y;

                    raw.Right = raw.Left + 50.0f;
                    raw.Bottom = raw.Top - 50.0f;
                    raw.Left -= 50.0f;
                    raw.Top += 50.0f;

                    target.DrawBitmap( (Bitmap)resCache["Bitmap"], raw, 1.0f - GetPsersent( item.TimeSpan.TotalMilliseconds ), BitmapInterpolationMode.Linear );

                }

                while ( this.Queue.Any() && this.Queue.Peek().TimeSpan > TimeSpan.FromMilliseconds( 1000 ) ) {

                    this.Queue.Dequeue();

                }

            }

        }

        public void OnReactionReceived( object sender, ReactionEventArgs eventArgs ) {

            lock ( this.Queue ) {

                this.Queue.Enqueue( new ReactionInfo( eventArgs.ReactionTypes ) );

            }

        }


        private float GetPsersent(double ms) {

            return (float)( ms / 1000.0 );

        }

        private (float x, float y) GetPosition(ReactionInfo info) {

            float y = GetPsersent( info.TimeSpan.TotalMilliseconds ) * ( (float)ActualHeight * 0.8f );

            return (0, ( y ));


        }

        private Bitmap GetBitmap(RenderTarget renderTarget) {

            using ( var bitmap = Properties.Resources.heart ) {
                var sourceArea = new System.Drawing.Rectangle( 0, 0, bitmap.Width, bitmap.Height );
                var bitmapProperties = new BitmapProperties( new PixelFormat( Format.R8G8B8A8_UNorm, AlphaMode.Premultiplied ) );
                var size = new Size2( bitmap.Width, bitmap.Height );

                // Transform pixels from BGRA to RGBA
                int stride = bitmap.Width * sizeof( int );
                using ( var tempStream = new DataStream( bitmap.Height * stride, true, true ) ) {
                    // Lock System.Drawing.Bitmap
                    var bitmapData = bitmap.LockBits( sourceArea, ImageLockMode.ReadOnly, System.Drawing.Imaging.PixelFormat.Format32bppPArgb );

                    // Convert all pixels 
                    for ( int y = 0; y < bitmap.Height; y++ ) {
                        int offset = bitmapData.Stride * y;
                        for ( int x = 0; x < bitmap.Width; x++ ) {
                            // Not optimized 
                            byte B = Marshal.ReadByte( bitmapData.Scan0, offset++ );
                            byte G = Marshal.ReadByte( bitmapData.Scan0, offset++ );
                            byte R = Marshal.ReadByte( bitmapData.Scan0, offset++ );
                            byte A = Marshal.ReadByte( bitmapData.Scan0, offset++ );
                            int rgba = R | ( G << 8 ) | ( B << 16 ) | ( A << 24 );
                            tempStream.Write( rgba );
                        }

                    }
                    bitmap.UnlockBits( bitmapData );
                    tempStream.Position = 0;

                    return new Bitmap( renderTarget, size, tempStream, stride, bitmapProperties );
                }
            }

        }

    }


}
