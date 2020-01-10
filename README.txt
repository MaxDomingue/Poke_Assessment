Upon running the program the User is prompted to input a Pokemon name.
The console will then output the Pokemon's strengths & weaknesses in the form of the damage relationships.

Unfortunately, I was unable to finish the application to my satisfaction.
Works in progress include:
	1) Breaking code up logically (breaking my classes out into seperate files).
	2) Implementing a DamageCalculator to more accurately and succinctly capture
		pokemon strengths and weaknesses. This is really only an issue with dual
		type pokemon. For instance, if a pokemon has two types and their first
		type takes double damage from water and their second type takes half damage,
		the pokemon only takes regular damage and is neither strong nor weak vs water.
		The damage relationships are multiplicative, so if both types took double vs 
		water, then the pokemon would take 4X as much dmg.
			(this is important if you gotta catch them all!)
	3) Implementing unit tests.
	4) Handle user input.