using Godot;
using System;

public partial class player : CharacterBody2D
{
	private AnimatedSprite2D _animatedSprite;
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		_animatedSprite = GetNode<AnimatedSprite2D>("AnimatedSprite2D");
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		if (Input.IsActionPressed("boostFwd"))
		{
			_animatedSprite.Play("fwd");
			Velocity = new Vector2(0,-10);
		}
		else
			_animatedSprite.Play("default");
		MoveAndSlide();
	}
}

