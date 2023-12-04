using Godot;
using System;

public partial class player : CharacterBody2D
{
    private const int LEFT = -1;
    private const int RIGHT = 1;

	private AnimatedSprite2D _animatedSprite;
	private bool _inputReceived;
	private const float  _LinearAcceleration = 1.0001f;//in px per frame 
	private int _linDelta = 0;

    private const float _AngularAccleration = 0.001f;
    private int _angDelta = 0;
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		_animatedSprite = GetNode<AnimatedSprite2D>("AnimatedSprite2D");
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		_inputReceived = false;
		if (Input.IsActionPressed("boostFwd"))
		{
			_animatedSprite.Play("fwd");
			_linDelta++;
			float velocity = _LinearAcceleration * _linDelta;
			GD.Print(velocity);
			Velocity = new Vector2(0,-velocity); //velocity is negiive bc it will go backward otherwise :)
			_inputReceived = true;
		}

        if (Input.IsActionPressed("turnL"))
        {
            _animatedSprite.Play("turnL");
            _angDelta--;
            _inputReceived = true;
        }

        if (Input.IsActionPressed("turnR"))
        {
            _animatedSprite.Play("turnR");
            _angDelta++;
            _inputReceived = true;
        }
        Rotation += _angDelta * _AngularAccleration;

		if(!_inputReceived)
			_animatedSprite.Play("default");

		MoveAndSlide();
	}
}
