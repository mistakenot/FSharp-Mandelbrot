namespace Mandelbrot

module Main = 
    open System
    open Gtk
    
    [<EntryPoint>]
    let Main(args) = 
        Application.Init()
        let path = @"/home/charlie/projects/Mandelbrot/Mandelbrot/image.jpeg"
        let pixbufImage = new Gdk.Pixbuf(path)
        let img = new Image(pixbufImage)
        let win = new MainWindow.MyWindow(img)
        win.Show()
        Application.Run()
        0

