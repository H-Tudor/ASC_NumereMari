using System;

namespace ASC_NumereMari {
	internal class Program {
		static void Main(string[] args) {
			BigNumber[,] TestCasesAddition = {
				{ BigNumber.ONE,			BigNumber.ONE,				BigNumber.TWO				},
				{ new BigNumber((long)1),	new BigNumber((long)1),		BigNumber.TWO				},
				{ new BigNumber((long)-1),	new BigNumber((long)-1),    new BigNumber((long)-2)		},
				{ new BigNumber((long)9),	new BigNumber((long)1),		BigNumber.TEN				},
				{ new BigNumber((long)19),	new BigNumber((long)1),		new BigNumber((long)20)		},

				{ new BigNumber((long)1),	new BigNumber((long)-1),	BigNumber.ZERO				},
				{ new BigNumber((long)0),	new BigNumber((long)-1),	new BigNumber((long)-1)		},
				{ BigNumber.TEN,			new BigNumber((long)-1),	new BigNumber((long)9)		},
				{ new BigNumber((long)-1),	BigNumber.TEN,				new BigNumber((long)9)		},
				{ new BigNumber((long)20),	new BigNumber((long)-1),	new BigNumber((long)19)		},
				{ new BigNumber((long)21),	new BigNumber((long)-1),	new BigNumber((long)20)		},
			};

			BigNumber[,] TestCasesSubtraction = {
				{ BigNumber.ONE,			BigNumber.ONE,              new BigNumber((long)-2)		},
				{ new BigNumber((long)1),   new BigNumber((long)1),     BigNumber.ZERO				},
				{ new BigNumber((long)-1),  new BigNumber((long)-1),    BigNumber.ZERO				},
				{ new BigNumber((long)10),	new BigNumber((long)1),		new BigNumber((long)9)		},
				{ new BigNumber((long)20),  new BigNumber((long)1),     new BigNumber((long)19)		},

				{ new BigNumber((long)1),   new BigNumber((long)-1),    BigNumber.TWO				},
				{ new BigNumber((long)0),   new BigNumber((long)-1),    BigNumber.ONE				},
				{ BigNumber.TEN,            new BigNumber((long)-1),    new BigNumber((long)11)		},
				{ new BigNumber((long)-1),  BigNumber.TEN,              new BigNumber((long)-11)	},
				{ new BigNumber((long)20),  new BigNumber((long)-1),    new BigNumber((long)21)		},
				{ new BigNumber((long)19),  new BigNumber((long)-1),    new BigNumber((long)20)		},
			};

			BigNumber[,] TestCasesMultiplication = {
				{ BigNumber.ONE,			BigNumber.ONE,				BigNumber.ONE				},
				{ BigNumber.ONE,			BigNumber.ZERO,				BigNumber.ZERO				},
				{ BigNumber.ZERO,			BigNumber.ONE,				BigNumber.ZERO				},

				{ new BigNumber((long)1),   new BigNumber((long)1),     BigNumber.ONE				},
				{ new BigNumber((long)-1),  new BigNumber((long)-1),    BigNumber.ONE				},
				{ new BigNumber((long)10),  new BigNumber((long)1),     BigNumber.TEN				},
				{ new BigNumber((long)10),  new BigNumber((long)-1),    new BigNumber((long)-10)		},
				{ new BigNumber((long)9),   new BigNumber((long)9),     new BigNumber((long)81)		},
				{ new BigNumber((long)99),	new BigNumber((long)99),	new BigNumber((long)9801)	},
			};

			bool ok, tc ;

			//ok = true;
			//Console.WriteLine("Addition");
			//for(int i = 0; i < TestCasesAddition.GetLength(0); i++) {
			//	tc = TestCasesAddition[i, 0] + TestCasesAddition[i, 1] == TestCasesAddition[i, 2];
			//	ok = !tc ? tc : ok;
			//	Console.WriteLine($"Test #{i + 1}: {TestCasesAddition[i, 0] + TestCasesAddition[i, 1]}: {TestCasesAddition[i, 2]} => {tc}");
			//}
			//Console.WriteLine($"Addition: {(ok ? "Passed" : "Failed")} \r\n");

			//ok = true;
			//Console.WriteLine("Subtraction");
			//for(int i = 0; i < TestCasesSubtraction.GetLength(0); i++) {
			//	tc = TestCasesSubtraction[i, 0] - TestCasesSubtraction[i, 1] == TestCasesSubtraction[i, 2];
			//	ok = !tc ? tc : ok;
			//	Console.WriteLine($"Test #{i + 1}: {TestCasesSubtraction[i, 0] - TestCasesSubtraction[i, 1]}: {TestCasesSubtraction[i, 2]} => {tc}");
			//}
			//Console.WriteLine($"Subtraction: {(ok ? "Passed" : "Failed")} \r\n");

			//ok = true;
			//Console.WriteLine("Multipication");
			//for(int i = 0; i < TestCasesMultiplication.GetLength(0); i++) {
			//	tc = TestCasesMultiplication[i, 0] * TestCasesMultiplication[i, 1] == TestCasesMultiplication[i, 2];
			//	ok = !tc ? tc : ok;
			//	Console.WriteLine($"Test #{i + 1}: {TestCasesMultiplication[i, 0] * TestCasesMultiplication[i, 1]}: {TestCasesMultiplication[i, 2]} => {tc}");
			//}
			//Console.WriteLine($"Multipication: {(ok ? "Passed" : "Failed")} \r\n");








			Console.WriteLine($"Test: {TestCasesMultiplication[8, 0] * TestCasesMultiplication[8, 1]}: {TestCasesMultiplication[8, 2]} => {TestCasesMultiplication[8, 0] * TestCasesMultiplication[8, 1] == TestCasesMultiplication[8, 2]}");








			Console.ReadKey();
		}
	}
}
