using System;

namespace RetroGameClasses.Text
{
	public enum Petscii
	{
		At,
		A,
		B,
		C,
		D,
		E,
		F,
		G,
		H,
		I,
		J,
		K,
		L,
		M,
		N,
		O,
		P,
		Q,
		R,
		S,
		T,
		U,
		V,
		W,
		X,
		Y,
		Z,
		LeftBracket,
		Pound,
		RightBracket,
		Up,
		Left,
		Space,
		Exclamation,
		Quote,
		Hash,
		Dollar,
		Percent,
		Amp,
		Apos,
		LeftParentheses,
		RightParentheses,
		Asterisk,
		Plus,
		Comma,
		Minus,
		FullStop,
		Slash,
		Num0,
		Num1,
		Num2,
		Num3,
		Num4,
		Num5,
		Num6,
		Num7,
		Num8,
		Num9,
		Colon,
		Semicolon,
		LessThan,
		EqualsTo,
		GreaterThan,
		Question,
		ShiftAt,
		ShiftA,
		ShiftB,
		ShiftC,
		ShiftD,
		ShiftE,
		ShiftF,
		ShiftG,
		ShiftH,
		ShiftI,
		ShiftJ,
		ShiftK,
		ShiftL,
		ShiftM,
		ShiftN,
		ShiftO,
		ShiftP,
		ShiftQ,
		ShiftR,
		ShiftS,
		ShiftT,
		ShiftU,
		ShiftV,
		ShiftW,
		ShiftX,
		ShiftY,
		ShiftZ,
		ShiftLeftBracket,
		ShiftPound,
		ShiftRightBracket,
		ShiftUp,
		ShiftLeft,
		ShiftSpace,
		ShiftExclamation,
		ShiftQuote,
		ShiftHash,
		ShiftDollar,
		ShiftPercent,
		ShiftAmp,
		ShiftApos,
		ShiftLeftParentheses,
		ShiftRightParentheses,
		ShiftAsterisk,
		ShiftPlus,
		ShiftComma,
		ShiftMinus,
		ShiftFullStop,
		ShiftSlash,
		ShiftNum0,
		ShiftNum1,
		ShiftNum2,
		ShiftNum3,
		ShiftNum4,
		ShiftNum5,
		ShiftNum6,
		ShiftNum7,
		ShiftNum8,
		ShiftNum9,
		ShiftColon,
		ShiftSemicolon,
		ShiftLessThan,
		ShiftEqualsTo,
		ShiftGreaterThan,
		ShiftQuestion
	}

	public enum CharacterSet
	{
		Uppercase,
		UppercaseInverted,
		Lowercase,
		LowercaseInverted
	}

	internal static class PetsciiHelper
	{
		internal static Petscii GetCharacter(char c)
		{
			switch (c)
			{
				case '@': return Petscii.At;
				case 'a': return Petscii.A;
				case 'b': return Petscii.B;
				case 'c': return Petscii.C;
				case 'd': return Petscii.D;
				case 'e': return Petscii.E;
				case 'f': return Petscii.F;
				case 'g': return Petscii.G;
				case 'h': return Petscii.H;
				case 'i': return Petscii.I;
				case 'k': return Petscii.K;
				case 'l': return Petscii.L;
				case 'm': return Petscii.M;
				case 'n': return Petscii.N;
				case 'o': return Petscii.O;
				case 'p': return Petscii.P;
				case 'r': return Petscii.R;
				case 's': return Petscii.S;
				case 't': return Petscii.T;
				case 'u': return Petscii.U;
				case 'v': return Petscii.V;
				case 'w': return Petscii.W;
				case 'x': return Petscii.X;
				case ':': return Petscii.Colon;
				case ',': return Petscii.Comma;
				case '-': return Petscii.Minus;
				case '.': return Petscii.FullStop;
				case '0': return Petscii.Num0;
				case '1': return Petscii.Num1;
				case '2': return Petscii.Num2;
				case '3': return Petscii.Num3;
				case '4': return Petscii.Num4;
				case '5': return Petscii.Num5;
				case '6': return Petscii.Num6;
				case '7': return Petscii.Num7;
				case '8': return Petscii.Num8;
				case '9': return Petscii.Num9;
				case ' ': return Petscii.Space;
				case '!': return Petscii.Exclamation;
				case '(': return Petscii.LeftParentheses;
				case ')': return Petscii.RightParentheses;
				case 'A': return Petscii.ShiftA;
				case 'B': return Petscii.ShiftB;
				case 'C': return Petscii.ShiftC;
				case 'D': return Petscii.ShiftD;
				case 'E': return Petscii.ShiftE;
				case 'P': return Petscii.ShiftP;
				case 'W': return Petscii.ShiftW;
				default: throw new ArgumentOutOfRangeException(c.ToString());
			}
		}
	}
}
