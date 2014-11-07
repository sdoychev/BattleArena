var speedRot : Vector3;
var values : int = 5;

function Update () {
	transform.Rotate(Vector3(Time.deltaTime*speedRot.x,Time.deltaTime*speedRot.y,Time.deltaTime*speedRot.z));
}