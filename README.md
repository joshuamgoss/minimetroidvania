# minimetroidvania

This is a project I began when I was learning C# and Unity.
!!! THE EXE FILE IS NOT CORRECT AND SHOULD NOT BE USED !!!
I have a copy of the game complied before the unity update that
broke it, and will update with that file when I can, but if this 
message is still up then I haven't made that correction yet.  It is on
an external hard drive...  somewhere.  I really wish I had
figured out git before I started this project back in 2016.

This is provided mainly to show my flexibility and resourcefulness.
I had not been progrmming in C# very long when I began this project 
and hadn't looked much into best practices.  As a result the code
is largely un-commented and debugging/testing structures aren't
in place.

I recomend against downloading the entire project.
I have included everything I will need to pick the project
back up later, including all graphic resources and audio files.

The project is a short 2-d metroidvania style game.  I used a
tiling method to create the levels, where 2d matrices store tile 
assignments and a second program details how the levels are rendered.
There are both pathing enemies and enemies with simple AI - all of which
was coded from scratch for this project.

Sprite sheets were made by building the characters in Blender, animating
using a motion capture database, then capturing key frames against a transparent
background to convert from 3d animated models to 2d sprite sheets.
GIMP was used for skinning the models and rigify was used for rigging.
Some simpler animations like the turrets tracking the player vertically
were made using the in-engine event animator.

