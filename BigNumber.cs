using System;
using System.Linq;

namespace ASC_NumereMari {
	public class BigNumber {
		byte[] Number;
		sbyte Sign;
		public static readonly BigNumber ZERO = new BigNumber(new byte[1] { 0 });
		public static readonly BigNumber ONE = new BigNumber(new byte[1] { 1 });
		public static readonly BigNumber TWO = new BigNumber(new byte[1] { 2 });
		public static readonly BigNumber TEN = new BigNumber(new byte[2] { 0, 1 });

		private BigNumber() {
			new BigNumber(new byte[1] { 0 });
		}

		public BigNumber(byte[] number) {
			Number = number;
			Sign = 1;
		}

		public BigNumber(byte[] number, sbyte sign) {
			Number = number;
			Sign = sign;
		}

		public BigNumber(string number) {
			Number = ConvertStringToNumber(number);
		}

		public BigNumber(long number) {
			switch(number) {
				case 0:
					Number = ZERO.Number;
					Sign = 1;
					break;
				case 1:
					Number = ONE.Number;
					Sign = 1;
					break;
				case 2:
					Number = TWO.Number;
					Sign = 1;
					break;
				case 10:
					Number = TEN.Number;
					Sign = 1;
					break;
				case -1:
					Number = ONE.Number;
					Sign = -1;
					break;
				case -2:
					Number = TWO.Number;
					Sign = -1;
					break;
				case -10:
					Number = TEN.Number;
					Sign = -1;
					break;
				default:
					Sign = (sbyte)Math.Sign(number);
					Number = new byte[(int)Math.Log10(Math.Abs(number)) + 1];

					for(int i = 0; i < Number.Length; i++) {
						Number[i] = (byte)Math.Abs(number % 10);
						number /= 10;
					}

					break;
			}
		}

		private byte[] ConvertStringToNumber(string number) {
			int sign = number.StartsWith("-") == true ? -1 : 0;
			Sign = number.StartsWith("-") == true ? (sbyte)-1 : (sbyte)1;

			int length = number.Length + sign;

			byte[] numberArray = new byte[length];
			char[] digits = number.Reverse().ToArray();

			for(int i = 0; i < length; i++) {
				byte.TryParse(digits[i].ToString(), out Number[i]);
			}

			return numberArray;
		}

		public override string ToString() {
			string number = "";

			if(Sign != 1) number += '-';

			for(int i = Number.Length - 1; i >= 0; i--) number += Number[i];

			return number;
		}

		public override bool Equals(object obj) {
			return base.Equals(obj);
		}

		public override int GetHashCode() {
			return base.GetHashCode();
		}

		public BigNumber Trim() {
			int length = 0;

			for(int i = Number.Length - 1; i >= 1; i--) {
				if(Number[i] == 0) {
					length++;
				} else {
					break;
				}
			}

			if(length != 0) {
				length = Number.Length - length;
				byte[] temp = new byte[length];

				for(int i = 0; i < length; i++) {
					temp[i] = Number[i];
				}

				return new BigNumber(temp, Sign);
			}

			return this;
		}

		public BigNumber Abs() {
			return new BigNumber(Number, 1);
		}

		public static bool operator ==(BigNumber a, BigNumber b) {
			if(a.Number == b.Number && a.Number == ZERO.Number) return true;

			if(a.Sign != b.Sign) return false;
			if(a.Number.Length != b.Number.Length) return false;

			for(int i = a.Number.Length - 1; i >= 0; i--) {
				if(a.Number[i] != b.Number[i]) return false;
			}

			return true;
		}

		public static bool operator !=(BigNumber a, BigNumber b) {
			return !(a == b);
		}

		public static bool operator <(BigNumber a, BigNumber b) {
			if(a.Sign < b.Sign) return true;
			else if(a.Sign > b.Sign) return false;

			if(a.Number.Length < b.Number.Length) return true;
			else if(a.Number.Length > b.Number.Length) return false;

			for(int i = a.Number.Length - 1; i >= 0; i--) {
				if(a.Number[i] < b.Number[i]) {
					return a.Sign < 0 ? false : true;
				} else if(a.Number[i] > b.Number[i]) {
					return a.Sign < 0 ? true : false;
				}
			}

			return true;
		}

		public static bool operator >(BigNumber a, BigNumber b) {
			return !(a == b || a < b);
		}

		public static bool operator <=(BigNumber a, BigNumber b) {
			return (a < b || a == b);
		}

		public static bool operator >=(BigNumber a, BigNumber b) {
			return (a == b || a > b);
		}

		public static BigNumber operator +(BigNumber a, BigNumber b) {
			if(a.Number.Length == 0 && b.Number.Length == 0) return ZERO;
			if(a.Abs() == b.Abs() && a.Sign != b.Sign) return ZERO;
			else if(b.Number.Length == 0) return a;
			else if(a.Number.Length == 0) return b;

			int sign = a.Sign * b.Sign;
			sbyte finalSign = a.Abs() >= b.Abs() ? a.Sign : b.Sign;

			if(sign == -1 && finalSign == -1 && a > b) (a, b) = (b, a);
			else if(sign == -1 && finalSign == 1 && a < b) (a, b) = (b, a);

			int maxLength = a.Number.Length >= b.Number.Length ? a.Number.Length + 1 : b.Number.Length + 1;
			int minLength = a.Number.Length <= b.Number.Length ? a.Number.Length : b.Number.Length;
			int deltaLength = maxLength - minLength;

			byte[] temp = new byte[maxLength];

			sbyte carry = 0, tempSum = 0;

			for(int i = 0; i < minLength; i++) {
				tempSum = (sbyte)(sign != 1 ? a.Number[i] - b.Number[i] : a.Number[i] + b.Number[i]);
				tempSum += carry;

				if(tempSum > -1 && tempSum < 10) {
					carry = 0;
					temp[i] = (byte)tempSum;
				} else {
					if(tempSum > 9) {
						carry = 1;
						temp[i] = (byte)(tempSum - 10);
					} else {
						carry = -1;
						temp[i] = (byte)(10 + tempSum);
					}
				}
			}

			for(int i = minLength; i < maxLength; i++) {
				if(i == maxLength - 1) {
					if(carry == 1) temp[i] = (byte)carry;
					break;
				} else {
					if(a.Number.Length + 1 == maxLength) {
						tempSum = (sbyte)a.Number[i];
					} else if(b.Number.Length + 1 == maxLength) {
						tempSum = (sbyte)b.Number[i];
					}
				}

				tempSum += carry;

				if(tempSum > -1 && tempSum < 10) {
					carry = 0;
					temp[i] = (byte)tempSum;
				} else {
					if(tempSum > 9) {
						carry = 1;
						temp[i] = (byte)(tempSum - 10);
					} else {
						carry = -1;
						temp[i] = (byte)(10 + tempSum);
					}
				}

			}

			if(sign == -1 && finalSign == -1 && a > b) (a, b) = (b, a);
			else if(sign == -1 && finalSign == 1 && a < b) (a, b) = (b, a);

			return new BigNumber(temp, finalSign).Trim();
		}

		public static BigNumber operator -(BigNumber a, BigNumber b) {
			b.Sign = (sbyte)(-1 * b.Sign);
			BigNumber sum = a + b;
			b.Sign = (sbyte)(-1 * b.Sign);
			return sum;
		}

		//// Multiplication by Iterative Addition
		//public static BigNumber operator *(BigNumber a, BigNumber b) {
		//	BigNumber aTemp = a.Abs(), bTemp = b.Abs();
		//	if(a == ZERO || b == ZERO) return ZERO;

		//	sbyte sign = (sbyte)(a.Sign * b.Sign);

		//	BigNumber k = ZERO, p = ZERO;

		//	for(k = ONE; k < bTemp; k += ONE) {
		//		p += aTemp;
		//	}

		//	p.Sign = sign;

		//	return p.Trim();
		//}

		public static BigNumber operator *(BigNumber a, BigNumber b) {
			if(a == ZERO || b == ZERO) return ZERO;
			if(b == ONE) return a;
			if(a == ONE) return b;

			sbyte sign = (sbyte)(a.Sign * b.Sign);
			byte[] temp = new byte[a.Number.Length + b.Number.Length];

			int tempProd = 0, tempSum = 0;
			sbyte carryProd = 0, carrySum = 0;
			int maxPos = 0;

			/*
					99 *
					99
					--
					81
				   81
				  81
			     81
			
				   891
				  891
				 -----
				  9801
			 
			 */

			for(int i = 0; i < b.Number.Length + 1; i++) {
				for(int j = 0; j < a.Number.Length; j++) {
					if(i == b.Number.Length) {
						temp[maxPos + 1] = (byte)(carryProd + carrySum);
						break;
					}
					tempProd = b.Number[i] * a.Number[j];
					tempProd += carryProd;
					carryProd = (sbyte)(tempProd / 10);

					tempSum = temp[i + j] + tempProd % 10 + carrySum;

					if(i + j > maxPos) maxPos = i + j;

					if(tempSum < 10) {
						carrySum = 0;
						temp[i + j] = (byte)tempSum;
					} else {
						if(tempSum > 9) {
							carrySum = 1;
							temp[i + j] = (byte)(tempSum - 10);
						}
					}
				}
			}

			return new BigNumber(temp, sign).Trim();
		}

		public static BigNumber operator /(BigNumber a, BigNumber b) {
			BigNumber aTemp = a.Abs(), bTemp = b.Abs();
			if(aTemp < bTemp || a == ZERO || b == ZERO) return ZERO;

			sbyte sign = (sbyte)(a.Sign * b.Sign);

			if(aTemp == bTemp) return new BigNumber(ONE.Number, sign);

			BigNumber k = ZERO;

			for(k = ZERO; aTemp >= bTemp; k += ONE) {
				aTemp -= bTemp;
			}

			k.Sign = sign;

			return k.Trim();
		}

		public static BigNumber operator %(BigNumber a, BigNumber b) {
			BigNumber aTemp = a.Abs(), bTemp = b.Abs();
			if(aTemp < bTemp || a == ZERO || b == ZERO) return ZERO;

			sbyte sign = (sbyte)(a.Sign * b.Sign);

			if(aTemp == bTemp) return ZERO;

			for(; aTemp >= bTemp;) {
				aTemp -= bTemp;
			}

			return aTemp.Trim();
		}


		public BigNumber Power(BigNumber a) {
			BigNumber power = ONE;

			for(BigNumber i = ONE; i <= a; i += ONE) {
				power *= this;
			}

			return power.Trim();
		}

		public static BigNumber Power(BigNumber a, BigNumber b) {
			return a.Power(b);
		}

		public BigNumber SQRT() {
			if(Sign == -1) return ZERO;

			bool found = false;
			BigNumber start = ZERO, end = this, mid = this / TWO, midTwo;

			while(!found) {
				midTwo = Power(mid, TWO);
				if(this >= midTwo) {
					if(this == midTwo) found = true;
					else {
						start = mid; mid = (start + end) / TWO;
					}
				} else {
					end = mid; mid = (start + end) / TWO;
				}
			}
			return mid;
		}

		public static BigNumber SQRT(BigNumber a) {
			return a.SQRT();
		}
	}
}
