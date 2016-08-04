namespace Mandelbrot

open System

module Calculate =

    type Result = 
        | Bounded
        | Diverged of int

    let isDivergent (power: float) (limit: int) (x0: float) (y0: float) =
        let rec isDivergentInner iteration x y =
            if iteration > limit then Bounded
            else
                if (x*x + y*y) > (power*power) then Diverged iteration
                else isDivergentInner (iteration + 1) (x*x - y*y + x0) (power*x*y + y0)
         
        isDivergentInner 0 0.0 0.0
