using Godot;
using System;

public partial class player : CharacterBody2D
{
	private AnimatedSprite2D _animatedSprite;
	private bool _inputReceived;
	private const float  _linearAcceleration = 1.0001f;//in px per lindelta
	private int _linDelta = 0;
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
			float velocity = _linearAcceleration * _linDelta;
			GD.Print(velocity);
			Velocity = new Vector2(0,-velocity); //velocity is negiive bc it will go backward otherwise :)
			_inputReceived = true;
		}


		if(!_inputReceived)
			_animatedSprite.Play("default");

		MoveAndSlide();
	}
}
