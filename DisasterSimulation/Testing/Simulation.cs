using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using SFML.System;
using SFML.Graphics;

namespace DisasterSimulation.Testing
{

    [TestFixture]
    public class Simulation
    {

        Data data;
        Sprite SEnv;
        Texture TTra;
        Image ITra;
        Sprite STra;
        Color EC;
        Color TC;
        Agent a;
        /*
         * Short hand setup for future tests
         */
        [OneTimeSetUp]
        public void TestFixtureSetup()
        {
            data = new Data();
            data.Initialize();
            a = new Agent();
            a.init(data, new Vector2f(0,0));

        }
        /*
         * Test to see agent initialisation correctly
         */
        [Test]
        public void agentInit()
        {
            TestFixtureSetup();
            Assert.NotNull(a.Position);
        }
        /*
         * Test for Byte Clamp always between 0 and 255
         */
        [Test]
        public void BClampTest()
        {
            for(int i =0; i < 1000; i++)
            {
                double mantissa = (data.rand.NextDouble() * 2.0) - 1.0;
                double exponent = Math.Pow(2.0, data.rand.Next(-126, 128));
                float randomnumber =  (float)(mantissa * exponent);
                Assert.GreaterOrEqual(a.BClamp(randomnumber), 0);
                Assert.LessOrEqual(a.BClamp(randomnumber), 255);
            }
        }
        /*
         * Test to see if offset is succesffuly calculated
         */
        [Test]
        public void CalculateOffsett()
        {
            TestFixtureSetup();



        }
        /*
         * Test to see if repair will always be between 0 and 1
         */
        [Test]
        public void CalculateRepairTest()
        {
            TestFixtureSetup();
            for (int i = 0; i < 100; i++)
            {
                int random = data.rand.Next();
            Color color1 = new Color((byte)(255 % random), (byte)(255 % random), (byte)(255 % random));
            data.Environment = new Image(100, 100, color1);


                Assert.GreaterOrEqual((decimal)a.CalculateRepair(), 0);
                Assert.LessOrEqual((decimal)a.CalculateRepair(), 1);
            }
        }
        /*
         * Test to ensure that the repair is correctly performed and agent datamap is updated.
         */
        [Test]
        public void PerformRepairTest()
        {
            TestFixtureSetup();
            for (int i = 0; i < 100; i++)
            {
                int random = data.rand.Next();
                Color color1 = new Color((byte)(255 % random), (byte)(255 % random), (byte)(255 % random));
                random = data.rand.Next();
                Color color2 = new Color((byte)(255 % random), (byte)(255 % random), (byte)(255 % random));
                a.setEC(new Color(0, 0, 0));
                a.setTC(new Color(0, 0, 0));

                Assert.AreEqual(a.BClamp(TC.B + 255), 255);
                Assert.AreEqual(a.getData().getPixel(0, 0).R, a.getTC().R);
                Assert.AreEqual(a.getData().getPixel(0, 0).G, a.getTC().G);
                Assert.AreEqual(a.getData().getPixel(0, 0).B, a.getTC().B);
            }

        }
        /*
         * Test to ensure that the That aid is correctly performed and agent datamap is updated.
         */
        [Test]
        public void PerformAidTest()
        {
            TestFixtureSetup();
            for (int i = 0; i < 100; i++)
            {
                int random = data.rand.Next();
                Color color1 = new Color((byte)(255 % random), (byte)(255 % random), (byte)(255 % random));
                random = data.rand.Next();
                Color color2 = new Color((byte)(255 % random), (byte)(255 % random), (byte)(255 % random));
                a.setEC(new Color(0,0,0));
                a.setTC(new Color(0, 0, 0));

                Assert.AreEqual(a.BClamp(TC.G + 255), 255);
                Assert.AreEqual(a.getData().getPixel(0, 0).R, a.getTC().R);
                Assert.AreEqual(a.getData().getPixel(0, 0).G, a.getTC().G);
                Assert.AreEqual(a.getData().getPixel(0, 0).B, a.getTC().B);
            }
        }

        /*
         * Test to see if repair will always be between 0 and 1
         */
        [Test]
        public void CalculateAidTest()
        {
            TestFixtureSetup();
            for (int i = 0; i < 100; i++)
            {
            int random = data.rand.Next();
            Color color1 = new Color((byte)(255 %random), (byte)(255 %random), (byte)(255%random));
            data.Environment = new Image(100,100,color1);
            Assert.GreaterOrEqual((decimal)a.CalculateAid(), 0);
            Assert.LessOrEqual((decimal)a.CalculateAid(), 1);
             }
        }
        /*
         * Test to ensure paintposition works as intended
         */
        [Test]
        public void paintPositionTest()
        {
            TestFixtureSetup();
            for (int i = 0; i < 100; i++)
            {
                int random = data.rand.Next();
                Color color1 = new Color((byte)(255 % random), (byte)(255 % random), (byte)(255 % random));
                a.info = color1;
                a.paintPosition();

                Assert.AreEqual(a.getData().getPixel(0, 0).R, a.info.R);
                Assert.AreEqual(a.getData().getPixel(0, 0).G, a.info.G);
                Assert.AreEqual(a.getData().getPixel(0, 0).B, a.info.B);
            }
        }
    }
}
