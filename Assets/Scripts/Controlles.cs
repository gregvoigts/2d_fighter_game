public class Controlles
{
    public string Move { get; set; }
    public string Jump { get; set; }
    public string Fire { get; set; }
    public string MoveArm { get; set; }

    public string Squat { get; set; }

    public Controlles(string move, string jump, string fire, string moveWeapon, string squat)
    {
        this.Move = move;
        this.Jump = jump;
        this.Fire = fire;
        this.MoveArm = moveWeapon;
        Squat = squat;
    }
}