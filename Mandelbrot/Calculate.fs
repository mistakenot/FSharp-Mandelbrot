namespace Mandelbrot

open System

module Calculate =

    type Divergence = { Iteration: int; Limit: int; }

    type Result = 
        | Bounded
        | Divergence of Divergence

    let isDivergent power limit x0 y0 =
        let rec isDivergentInner iteration x y =
            if iteration > limit then Bounded
            else
                if (x*x + y*y) > (power*power) then Divergence { Iteration = iteration; Limit = limit; }
                else isDivergentInner (iteration + 1) (x*x - y*y + x0) (2*x*y + x0)
         
        isDivergentInner 0 x0 y0