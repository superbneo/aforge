// AForge Neural Net Library
//
// Copyright � Andrew Kirillov, 2005-2006
// andrew.kirillov@gmail.com
//

namespace AForge.Neuro.Learning
{
	using System;

	/// <summary>
	/// Summary description for PerceptronLearning.
	/// </summary>
	public class PerceptronLearning : ISupervisedLearning
	{
		// perceptron to teach
		private ActivationNeuron perceptron;
		// learning rate
		private double learningRate = 0.1;

		/// <summary>
		/// Learning rate
		/// </summary>
		/// 
		/// <remarks>The value determines speed of learning</remarks>
		/// 
		public double LearningRate
		{
			get { return learningRate; }
			set { learningRate = value; }
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="PerceptronLearning"/> class
		/// </summary>
		/// 
		/// <param name="perceptron">Perceptron to teach</param>
		/// 
		public PerceptronLearning( ActivationNeuron perceptron )
		{
			this.perceptron = perceptron;
		}

		/// <summary>
		/// Runs learning iteration
		/// </summary>
		/// 
		/// <param name="input">input vector</param>
		/// <param name="output">desired output vector</param>
		/// 
		/// <returns>Returns absolute error</returns>
		/// 
		/// 
		/// 
		public double Run( double[] input, double[] output )
		{
			// compute output of our perceptron
			double perceptronOutput = perceptron.Compute( input );

			// compute error
			double error = output[0] - perceptronOutput;

			// check error
			if ( error != 0 )
			{
				// update weights
				for ( int i = 0, n = perceptron.InputsCount; i < n; i++ )
				{
					perceptron[i] += learningRate * error * input[i];
				}

				// update threshold value
				perceptron.Threshold += learningRate * error;

				// make error to be absolute
				error = Math.Abs( error );
			}

			return error;
		}

	
		/// <summary>
		/// Runs learning epoch
		/// </summary>
		/// 
		/// <param name="input">array of input vectors</param>
		/// <param name="output">array of output vectors</param>
		/// 
		/// <returns>Returns sum of absolute errors</returns>
		/// 
		/// 
		/// 
		public double RunEpoch( double[][] input, double[][] output )
		{
			double error = 0.0;

			// run learning procedure for all samples
			for ( int i = 0, n = input.Length; i < n; i++ )
			{
				error += Run( input[i], output[i] );
			}

			// return summary error
			return error;
		}
	}
}