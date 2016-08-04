namespace Mandelbrot

module MainWindow = 
    open System
    open Gtk
    
    type MyWindow(image: Image) as this = 
        inherit Window("MainWindow")

        let gc = new Gdk.GC(this.GdkWindow)
        let mutable img = image
        let vbox = new VBox(false, 8)
        do vbox.BorderWidth <- 8ul
        do this.Add(vbox)

        let frame = new Frame()
        let alignment = new Alignment(0.5f, 0.5f, 0.0f, 0.0f)
        do alignment.Add(frame)
        do vbox.PackStart(alignment, false, false, 0ul)
        do frame.Add(img)

        do this.SetDefaultSize(400, 300)
        do this.DeleteEvent.AddHandler(fun o e -> this.OnDeleteEvent(o, e))
        do this.ShowAll()
       
        member this.SetImage image =
            img <- image

        member this.OnDeleteEvent(o, e : DeleteEventArgs) = 
            Application.Quit()
            e.RetVal <- true

        member __.Draw() =

           

