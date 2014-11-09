//scroll main texture based on time
 
 public var scrollSpeed : float = .5;
 public var offset : float;
 public var rotate : float;
 
 function Update (){
     offset+= (Time.deltaTime*scrollSpeed)/10.0;
     renderer.material.SetTextureOffset ("_MainTex", Vector2(offset,0));
 
 }