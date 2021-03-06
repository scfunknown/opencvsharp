﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using OpenCvSharp;

// for Binarizer
using OpenCvSharp.Extensions;
using SampleBase;

namespace CStyleSamplesCS
{
    /// <summary>
    /// Various Binarization Methods
    /// </summary>
    class BinarizationMethods
    {
        public BinarizationMethods()
        {
            using (var imgSrc = new IplImage(FilePath.Image.Binarization, LoadMode.GrayScale))
            using (var imgGauss = imgSrc.Clone())
            using (var imgNiblack = new IplImage(imgSrc.Size, BitDepth.U8, 1))
            using (var imgSauvola = new IplImage(imgSrc.Size, BitDepth.U8, 1))
            using (var imgBernsen = new IplImage(imgSrc.Size, BitDepth.U8, 1))
            {
                //Cv.Smooth(imgSrc, imgGauss, SmoothType.Gaussian, 9);
                //Cv.EqualizeHist(imgGauss, imgGauss);

                Stopwatch sw = Stopwatch.StartNew();
                {
                    const int Size = 31;
                    const double K = 0.2;
                    Binarizer.NiblackFast(imgGauss, imgNiblack, Size, K);
                }
                Console.WriteLine("Niblack: {0}ms", sw.ElapsedMilliseconds);
                sw.Reset(); sw.Start();
                {
                    const int Size = 31;
                    const double K = 0.9;
                    const double R = 32;
                    Binarizer.SauvolaFast(imgGauss, imgSauvola, Size, K, R);
                }
                Console.WriteLine("Sauvola: {0}ms", sw.ElapsedMilliseconds); 
                sw.Reset(); sw.Start();
                {
                    const int Size = 31;
                    const byte ContrastMin = 15;
                    const byte BgThreshold = 127;
                    Binarizer.Bernsen(imgGauss, imgBernsen, Size, ContrastMin, BgThreshold);
                }
                Console.WriteLine("bernsen: {0}ms", sw.ElapsedMilliseconds);

                using (new CvWindow("src", imgSrc))
                //using (new CvWindow("gauss", imgGauss))
                using (new CvWindow("niblack", imgNiblack))
                using (new CvWindow("sauvola", imgSauvola))
                using (new CvWindow("bernsen", imgBernsen))
                {
                    Cv.WaitKey();
                }
            }
        }
    }
}