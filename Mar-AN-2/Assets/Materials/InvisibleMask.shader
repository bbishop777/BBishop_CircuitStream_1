Shader "Custom/InvisibleMask"
{
    /*Properties
    {
        _Color ("Color", Color) = (1,1,1,1)         //Values for Red, Green, Blue, and Alpha
        _MainTex ("Albedo (RGB)", 2D) = "white" {}
        _Glossiness ("Smoothness", Range(0,1)) = 0.5
        _Metallic ("Metallic", Range(0,1)) = 0.0
    }*/
    SubShader  //Acts as a funciton         
    {
        Tags {"Queue" = "Transparent+1"}
        //We are adjusting a Render Queue (the order things are rendered).  Whatever we apply this to will put its Queue at
        //3000 (where transparancy is rendered) plus one, so 3001.  As to order of Queue rendering...the skybox (everything
        //in the distance like the sky, sun, etc) is at around 1000.  The next highest is the geometry (our cubes, 3D models,
        //etc.) is at 2000, then there is something called the Alpha Test (similar to transparency but used for testing) set
        //at around 2500, , then all transparent things are rendered (glass,
        //clear water, etc) and this is at 3000. The next highest render queue is user interface elements like buttons and
        //this is around 4000.  This setup allows the developer to use the values in between to assign open render queues. So
        //in this example we are setting our transparent plane at 3001 and then setting our Anatomy at around 3002 (keeping
        //these 2 things away from everything else).  The reason we do this is if create a transparent object at 3001 and any
        //object behind that at 3002 (at a higher render queue, for example) the pass filter will see that object or anything
        //with a higher render queue.  That is the concept with blends.  But this is just the camera looking thru the geometry
        //of the transparent object. Beyond its boundaries, any object with a higer render queue will still be seen or any part
        //of the object beyond the transparent object's boundaries

        Pass {Blend Zero One}
    }
}
