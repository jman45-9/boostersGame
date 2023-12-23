using Godot;
using System;

public partial class player : CharacterBody2D
{
	private bool isLeft;
	private bool lastIsLeft;
	private bool isUp;
	private bool lastIsUp;

	private double lastInputRot;

	private AnimatedSprite2D _animatedSprite;
	private bool _inputReceived;
	private const float  _LinearAcceleration = 1.0009f;//in px per frame 
	private int _xlinDelta = 0;
	private int _ylinDelta = 0;

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
		if(_xlinDelta < 0)
			_xlinDelta = 0;
		if(_ylinDelta < 0)
			_ylinDelta = 0;

		_inputReceived = false;
		if (Input.IsActionPressed("boostFwd"))
		{
			_animatedSprite.Play("fwd");
			this._changeDeltas();

			
			float xvelocity = _getXvel();
			float yvelocity = _getYvel();
			
			Velocity = new Vector2((float)(xvelocity * Math.Sin(Rotation)),(float)(-yvelocity * Math.Cos(Rotation))); //velocity is negiive bc it will go backward otherwise :)
			lastInputRot = Rotation;

			lastIsLeft = isLeft;
			lastIsUp = isUp;
			if(yvelocity *  Math.Sin(Rotation)<=0)
				isLeft = true;	
			else 
				isLeft = false;

			if(-xvelocity * Math.Cos(Rotation)<=0)
				isUp = true;
			else
				isUp = false;

			_inputReceived = true;
		}
		else
			Velocity = new Vector2((float)(this._getXvel() * Math.Sin(lastInputRot)),(float)(-this._getYvel()* Math.Cos(lastInputRot)));

		if (Input.IsActionPressed("turnL"))
		{
			_animatedSprite.Play("turnL");
			_angDelta--;
			_inputReceived = true;
			_xlinDelta--;
			_ylinDelta--;
		}

		if (Input.IsActionPressed("turnR"))
		{
			_animatedSprite.Play("turnR");
			_angDelta++;
			_inputReceived = true;
			_xlinDelta--;
			_ylinDelta--;


		}
		Rotation += _angDelta * _AngularAccleration;

		if(!_inputReceived)
			_animatedSprite.Play("default");
		MoveAndSlide();
	}
	private void _changeDeltas()
	{
		if (lastIsLeft == isLeft)
			_xlinDelta++;
		else
			_xlinDelta++;

		if (lastIsUp == isUp)
			_ylinDelta++;
		else
			_ylinDelta--;
	}

	private float _getXvel()
	{
		return _LinearAcceleration * _xlinDelta;
	}
	private float _getYvel()
	{
		return _LinearAcceleration * _ylinDelta;
	}


}
