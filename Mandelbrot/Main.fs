namespace Mandelbrot

module Main = 
    open System
    open System.Drawing
    open System.IO
    open Gtk

    type Pixel = {X: int; Y: int; IsBlack: bool}

    [<EntryPoint>]
    let Main(args) = 
        let isInSet: (float -> float -> Calculate.Result) = Calculate.isDivergent 2.0 1000

        let absoluteToRel x y =
            let newX = (x + 2.0) * 100.0
            let newY = (y + 1.0) * 100.0
            ((int)newX, (int)newY)

        let pixels = [|
            for x in -2.0..0.01..0.99 do
                for y in -1.0..0.01..0.99 do
                    let result = 
                        match isInSet x y with
                        | Calculate.Result.Bounded -> true
                        | Calculate.Result.Diverged d -> false
                    let locX, locY = absoluteToRel x y
                    yield { X = locX; Y = locY; IsBlack = result; } |]
        

        Application.Init()
        let path = @"/home/charlie/projects/Mandelbrot/Mandelbrot/image.jpeg"

        let bm = new System.Drawing.Bitmap(300, 200)

        for pixel in pixels do
            let colour = if pixel.IsBlack then Color.Black else Color.White
            bm.SetPixel(pixel.X, pixel.Y, colour)
        
        let filename = DateTime.Now.ToShortTimeString() + ".bmp"
        use file = File.OpenWrite(filename)
        use stream = new MemoryStream()
        bm.Save(stream, Imaging.ImageFormat.Png)
        bm.Save(file, Imaging.ImageFormat.Bmp)

        stream.Position <- 0L

        let pixbufImage = new Gdk.Pixbuf(stream)

        let img = new Gtk.Image(pixbufImage)
        let win = new MainWindow.MyWindow(img)
        win.Draw()
        win.Show()
        Application.Run()
        0

