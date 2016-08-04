namespace Mandelbrot

module MainWindow = 
    open System
    open Gtk
    
    type MyWindow(image: Image) as this = 
        inherit Window("MainWindow")
        
        let mutable img = image

        let frame = new Frame()
        let alignment = new Alignment(0.5f, 0.5f, 0.0f, 0.0f)
        do alignment.Add(frame)
        let vbox = new VBox(false, 8)
        do frame.Add(img)
        do vbox.PackStart(alignment, false, false, 0ul)
        do this.Add(vbox)

        do this.SetDefaultSize(300, 200)
        do this.DeleteEvent.AddHandler(fun o e -> this.OnDeleteEvent(o, e))
        do this.ShowAll()

        member __.Draw() = 
            let gc = new Gdk.GC(this.GdkWindow)
            gc.RgbFgColor <- new Gdk.Color(255uy, 50uy, 50uy)
            gc.RgbBgColor <- new Gdk.Color(0uy, 0uy, 0uy)
            gc.SetLineAttributes(6, Gdk.LineStyle.DoubleDash, Gdk.CapStyle.Projecting, Gdk.JoinStyle.Round)
            let vals = [|
                yield new Gdk.Point(10, 10)
                yield new Gdk.Point(20, 20)
                yield new Gdk.Point(30, 30)
                yield new Gdk.Point(100, 100)
            |]
            base.GdkWindow.DrawLines(gc, vals)

            ()

        member this.OnDeleteEvent(o, e : DeleteEventArgs) = 
            Application.Quit()
            e.RetVal <- true

        member this.OnExposeEvent(evnt) =
            base.OnExposeEvent(evnt) |> ignore
            this.Draw()
            ()