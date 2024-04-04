using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using System;

namespace Cheesenaf
{
    internal class Office
    {
        public int frametime;
        public bool Debug;
        public Texture2D[] officeSprites;
        public Texture2D[] jumpscareSprites;
        public Texture2D[] camSprites;
        public Texture2D theOffice;
        public Texture2D Animatronic;
        public Texture2D Buttons;
        public int framecounter;
        public int multiplier;
        public int officex;
        public int camsx;
        public int secretcamsx;
        public bool addcamsx;
        public SoundEffect[] sounds;
        public SoundEffect call;
        public Song[] ambience;
        public bool soundToggle;
        public SoundEffectInstance[] soundchannel;
        public Random rng;
        public bool isJumpScare;

        public bool inCams;
        public bool canCam;
        public int[] camStates;
        public int[] baseCam;
        public int currentCam;
        public int staticMultiplier;
        public string[] camNames;
        public float brokenCam = 0;
        public float brokenCamCooldown = 0;
        public float camTime; //for foxy

        public bool syowen;
        public bool mocha;
        public bool brett;
        public bool alan;
        public float bbgLookTime;
        public bool bbgTooLate;
        public float bbgAI;

        public bool Victory = false;
        public float victoryCheerTime = 6.5f;

        public bool bonnieAtDoor;
        public bool seenBonnie;
        public bool chicaAtDoor;
        public bool seenChica;
        public int nextMultiplier;
        public bool transition;
        public bool transitionFinish;
        public bool cheeseLeftDoor;
        public bool cheeseRightDoor;
        public bool seenCheese;

        public bool leftDoor;
        public bool rightDoor;
        public bool leftLight;
        public bool rightLight;
        public bool centerLight;

        public int jumpscareID;
        public int AnimatronicTimer;
        public int BonniePos;
        public int BonnieAI;
        public int ChicaPos;
        public int ChicaAI;
        public int FreddyPos;
        public int FreddyAI;
        public bool FreddyTrigger;
        public int FoxyPos;
        public int FoxyAI;
        public float FoxyTimer;
        public int FoxyAttacks;
        public int CheeseAI;
        public int CheesePos;
        public bool CheeseSound;
        //for 2 am, 3 am, 4 am
        public bool updateone;
        public bool updatetwo;
        public bool updatethree;

        public float time;
        public float power = 100;
        public int usage = 1;
        public int night = 1;
        public float clockAnimOffset = 540;

        float freddyPowerTimer;
        int freddyPowerPos;
        bool powerOutBegin;

        public MouseState mouseState;
        public int MouseX;
        public int MouseY;

        public int chunkLoad;
        public bool loaded;

        float phoneRingTimer = 3;
        bool phoneAnswered;
        bool secondphonebool;
        float phoneDelay = 1;

        bool stopSound;

        Game1 Game1;
        public void LoadContent(ContentManager Content)
        {
            switch (chunkLoad) //Chunk load 5 sections of office per frame or 1 jumpscare per frame
            {
                case 0:
                    //No light, No door 0
                    officeSprites = new Texture2D[253];
                    officeSprites[0] = Content.Load<Texture2D>("Office Renders/0001");
                    officeSprites[1] = Content.Load<Texture2D>("Office Renders/0002");
                    officeSprites[2] = Content.Load<Texture2D>("Office Renders/0003");
                    officeSprites[3] = Content.Load<Texture2D>("Office Renders/0004");
                    officeSprites[4] = Content.Load<Texture2D>("Office Renders/0005");
                    officeSprites[5] = Content.Load<Texture2D>("Office Renders/0006");
                    officeSprites[6] = Content.Load<Texture2D>("Office Renders/0007");
                    officeSprites[7] = Content.Load<Texture2D>("Office Renders/0008");
                    officeSprites[8] = Content.Load<Texture2D>("Office Renders/0009");
                    officeSprites[9] = Content.Load<Texture2D>("Office Renders/0010");
                    officeSprites[10] = Content.Load<Texture2D>("Office Renders/0011");
                    //Left light, no door 1
                    officeSprites[11] = Content.Load<Texture2D>("Office Renders/left light0001");
                    officeSprites[12] = Content.Load<Texture2D>("Office Renders/left light0002");
                    officeSprites[13] = Content.Load<Texture2D>("Office Renders/left light0003");
                    officeSprites[14] = Content.Load<Texture2D>("Office Renders/left light0004");
                    officeSprites[15] = Content.Load<Texture2D>("Office Renders/left light0005");
                    officeSprites[16] = Content.Load<Texture2D>("Office Renders/left light0006");
                    officeSprites[17] = Content.Load<Texture2D>("Office Renders/left light0007");
                    officeSprites[18] = Content.Load<Texture2D>("Office Renders/left light0008");
                    officeSprites[19] = Content.Load<Texture2D>("Office Renders/left light0009");
                    officeSprites[20] = Content.Load<Texture2D>("Office Renders/left light0010");
                    officeSprites[21] = Content.Load<Texture2D>("Office Renders/left light0011");
                    //Right light, no door 2
                    officeSprites[22] = Content.Load<Texture2D>("Office Renders/right light0001");
                    officeSprites[23] = officeSprites[22];
                    officeSprites[24] = officeSprites[22];
                    officeSprites[25] = officeSprites[22];
                    officeSprites[26] = officeSprites[22];
                    officeSprites[27] = officeSprites[22];
                    officeSprites[28] = officeSprites[22];
                    officeSprites[29] = officeSprites[22];
                    officeSprites[30] = officeSprites[22];
                    officeSprites[31] = officeSprites[22];
                    officeSprites[32] = officeSprites[22];
                    //Center light, no door 3
                    officeSprites[33] = Content.Load<Texture2D>("Office Renders/center light0001");
                    officeSprites[34] = officeSprites[33];
                    officeSprites[35] = officeSprites[33];
                    officeSprites[36] = officeSprites[33];
                    officeSprites[37] = officeSprites[33];
                    officeSprites[38] = officeSprites[33];
                    officeSprites[39] = officeSprites[33];
                    officeSprites[40] = officeSprites[33];
                    officeSprites[41] = officeSprites[33];
                    officeSprites[42] = officeSprites[33];
                    officeSprites[43] = officeSprites[33];
                    //Left light, no door, bonnie 4
                    officeSprites[44] = Content.Load<Texture2D>("Office Renders/left light bonnie0001");
                    officeSprites[45] = officeSprites[44];
                    officeSprites[46] = officeSprites[44];
                    officeSprites[47] = officeSprites[44];
                    officeSprites[48] = officeSprites[44];
                    officeSprites[49] = officeSprites[44];
                    officeSprites[50] = officeSprites[44];
                    officeSprites[51] = officeSprites[44];
                    officeSprites[52] = officeSprites[44];
                    officeSprites[53] = officeSprites[44];
                    officeSprites[54] = officeSprites[44];
                    break;
                case 1:
                    //Left door up 5
                    officeSprites[55] = Content.Load<Texture2D>("Office Renders/left door up0001");
                    officeSprites[56] = Content.Load<Texture2D>("Office Renders/left door up0002");
                    officeSprites[57] = Content.Load<Texture2D>("Office Renders/left door up0003");
                    officeSprites[58] = Content.Load<Texture2D>("Office Renders/left door up0004");
                    officeSprites[59] = Content.Load<Texture2D>("Office Renders/left door up0005");
                    officeSprites[60] = Content.Load<Texture2D>("Office Renders/left door up0006");
                    officeSprites[61] = Content.Load<Texture2D>("Office Renders/left door up0007");
                    officeSprites[62] = Content.Load<Texture2D>("Office Renders/left door up0008");
                    officeSprites[63] = Content.Load<Texture2D>("Office Renders/left door up0009");
                    officeSprites[64] = Content.Load<Texture2D>("Office Renders/left door up0010");
                    officeSprites[65] = Content.Load<Texture2D>("Office Renders/left door up0011");
                    //No light, left door 6
                    officeSprites[66] = Content.Load<Texture2D>("Office Renders/left door0001");
                    officeSprites[67] = officeSprites[66];
                    officeSprites[68] = officeSprites[66];
                    officeSprites[69] = officeSprites[66];
                    officeSprites[70] = officeSprites[66];
                    officeSprites[71] = officeSprites[66];
                    officeSprites[72] = officeSprites[66];
                    officeSprites[73] = officeSprites[66];
                    officeSprites[74] = officeSprites[66];
                    officeSprites[75] = officeSprites[66];
                    officeSprites[76] = officeSprites[66];
                    //Left light, left door 7
                    officeSprites[77] = Content.Load<Texture2D>("Office Renders/left door light0001");
                    officeSprites[78] = officeSprites[77];
                    officeSprites[79] = officeSprites[77];
                    officeSprites[80] = officeSprites[77];
                    officeSprites[81] = officeSprites[77];
                    officeSprites[82] = officeSprites[77];
                    officeSprites[83] = officeSprites[77];
                    officeSprites[84] = officeSprites[77];
                    officeSprites[85] = officeSprites[77];
                    officeSprites[86] = officeSprites[77];
                    officeSprites[87] = officeSprites[77];
                    //Left light, left door, bonnie 8
                    officeSprites[88] = Content.Load<Texture2D>("Office Renders/left door bonnie0001");
                    officeSprites[89] = officeSprites[88];
                    officeSprites[90] = officeSprites[88];
                    officeSprites[91] = officeSprites[88];
                    officeSprites[92] = officeSprites[88];
                    officeSprites[93] = officeSprites[88];
                    officeSprites[94] = officeSprites[88];
                    officeSprites[95] = officeSprites[88];
                    officeSprites[96] = officeSprites[88];
                    officeSprites[97] = officeSprites[88];
                    officeSprites[98] = officeSprites[88];
                    //Left door down 9
                    officeSprites[99] = Content.Load<Texture2D>("Office Renders/left door down0001");
                    officeSprites[100] = Content.Load<Texture2D>("Office Renders/left door down0002");
                    officeSprites[101] = Content.Load<Texture2D>("Office Renders/left door down0003");
                    officeSprites[102] = Content.Load<Texture2D>("Office Renders/left door down0004");
                    officeSprites[103] = Content.Load<Texture2D>("Office Renders/left door down0005");
                    officeSprites[104] = Content.Load<Texture2D>("Office Renders/left door down0006");
                    officeSprites[105] = Content.Load<Texture2D>("Office Renders/left door down0007");
                    officeSprites[106] = Content.Load<Texture2D>("Office Renders/left door down0008");
                    officeSprites[107] = Content.Load<Texture2D>("Office Renders/left door down0009");
                    officeSprites[108] = Content.Load<Texture2D>("Office Renders/left door down0010");
                    officeSprites[109] = Content.Load<Texture2D>("Office Renders/left door down0011");
                    break;
                case 2:
                    //Center Light, chica 10
                    officeSprites[110] = Content.Load<Texture2D>("Office Renders/center chica");
                    officeSprites[111] = officeSprites[110];
                    officeSprites[112] = officeSprites[110];
                    officeSprites[113] = officeSprites[110];
                    officeSprites[114] = officeSprites[110];
                    officeSprites[115] = officeSprites[110];
                    officeSprites[116] = officeSprites[110];
                    officeSprites[117] = officeSprites[110];
                    officeSprites[118] = officeSprites[110];
                    officeSprites[119] = officeSprites[110];
                    officeSprites[120] = officeSprites[110];
                    //Right light chica 11
                    officeSprites[121] = Content.Load<Texture2D>("Office Renders/right light chica");
                    officeSprites[122] = officeSprites[121];
                    officeSprites[123] = officeSprites[121];
                    officeSprites[124] = officeSprites[121];
                    officeSprites[125] = officeSprites[121];
                    officeSprites[126] = officeSprites[121];
                    officeSprites[127] = officeSprites[121];
                    officeSprites[128] = officeSprites[121];
                    officeSprites[129] = officeSprites[121];
                    officeSprites[130] = officeSprites[121];
                    officeSprites[131] = officeSprites[121];
                    //Right door chica 12
                    officeSprites[132] = officeSprites[121];
                    officeSprites[133] = officeSprites[132];
                    officeSprites[134] = officeSprites[132];
                    officeSprites[135] = officeSprites[132];
                    officeSprites[136] = officeSprites[132];
                    officeSprites[137] = officeSprites[132];
                    officeSprites[138] = officeSprites[132];
                    officeSprites[139] = officeSprites[132];
                    officeSprites[140] = officeSprites[132];
                    officeSprites[141] = officeSprites[132];
                    officeSprites[142] = officeSprites[132];
                    //Right door up 13
                    officeSprites[143] = Content.Load<Texture2D>("Office Renders/right door up0001");
                    officeSprites[144] = Content.Load<Texture2D>("Office Renders/right door up0002");
                    officeSprites[145] = Content.Load<Texture2D>("Office Renders/right door up0003");
                    officeSprites[146] = Content.Load<Texture2D>("Office Renders/right door up0004");
                    officeSprites[147] = Content.Load<Texture2D>("Office Renders/right door up0005");
                    officeSprites[148] = Content.Load<Texture2D>("Office Renders/right door up0006");
                    officeSprites[149] = Content.Load<Texture2D>("Office Renders/right door up0007");
                    officeSprites[150] = Content.Load<Texture2D>("Office Renders/right door up0008");
                    officeSprites[151] = Content.Load<Texture2D>("Office Renders/right door up0009");
                    officeSprites[152] = Content.Load<Texture2D>("Office Renders/right door up0010");
                    officeSprites[153] = Content.Load<Texture2D>("Office Renders/right door up0011");
                    //Right door down 14
                    officeSprites[154] = Content.Load<Texture2D>("Office Renders/right door down0001");
                    officeSprites[155] = Content.Load<Texture2D>("Office Renders/right door down0002");
                    officeSprites[156] = Content.Load<Texture2D>("Office Renders/right door down0003");
                    officeSprites[157] = Content.Load<Texture2D>("Office Renders/right door down0004");
                    officeSprites[158] = Content.Load<Texture2D>("Office Renders/right door down0005");
                    officeSprites[159] = Content.Load<Texture2D>("Office Renders/right door down0006");
                    officeSprites[160] = Content.Load<Texture2D>("Office Renders/right door down0007");
                    officeSprites[161] = Content.Load<Texture2D>("Office Renders/right door down0008");
                    officeSprites[162] = Content.Load<Texture2D>("Office Renders/right door down0009");
                    officeSprites[163] = Content.Load<Texture2D>("Office Renders/right door down0010");
                    officeSprites[164] = Content.Load<Texture2D>("Office Renders/right door down0011");
                    break;
                case 3:
                    //Right door 15
                    officeSprites[165] = Content.Load<Texture2D>("Office Renders/right door0001");
                    officeSprites[166] = officeSprites[165];
                    officeSprites[167] = officeSprites[165];
                    officeSprites[168] = officeSprites[165];
                    officeSprites[169] = officeSprites[165];
                    officeSprites[170] = officeSprites[165];
                    officeSprites[171] = officeSprites[165];
                    officeSprites[172] = officeSprites[165];
                    officeSprites[173] = officeSprites[165];
                    officeSprites[174] = officeSprites[165];
                    officeSprites[175] = officeSprites[165];
                    //Right door light 16
                    officeSprites[176] = Content.Load<Texture2D>("Office Renders/right door light0001");
                    officeSprites[177] = officeSprites[176];
                    officeSprites[178] = officeSprites[176];
                    officeSprites[179] = officeSprites[176];
                    officeSprites[180] = officeSprites[176];
                    officeSprites[181] = officeSprites[176];
                    officeSprites[182] = officeSprites[176];
                    officeSprites[183] = officeSprites[176];
                    officeSprites[184] = officeSprites[176];
                    officeSprites[185] = officeSprites[176];
                    officeSprites[186] = officeSprites[176];
                    //Center window freddy 17
                    officeSprites[187] = Content.Load<Texture2D>("Office Renders/fedy");
                    officeSprites[188] = officeSprites[187];
                    officeSprites[189] = officeSprites[187];
                    officeSprites[190] = officeSprites[187];
                    officeSprites[191] = officeSprites[187];
                    officeSprites[192] = officeSprites[187];
                    officeSprites[193] = officeSprites[187];
                    officeSprites[194] = officeSprites[187];
                    officeSprites[195] = officeSprites[187];
                    officeSprites[196] = officeSprites[187];
                    officeSprites[197] = officeSprites[187];
                    //Center window freddy and chica 18
                    officeSprites[198] = Content.Load<Texture2D>("Office Renders/chica fedy");
                    officeSprites[199] = officeSprites[198];
                    officeSprites[200] = officeSprites[198];
                    officeSprites[201] = officeSprites[198];
                    officeSprites[202] = officeSprites[198];
                    officeSprites[203] = officeSprites[198];
                    officeSprites[204] = officeSprites[198];
                    officeSprites[205] = officeSprites[198];
                    officeSprites[206] = officeSprites[198];
                    officeSprites[207] = officeSprites[198];
                    officeSprites[208] = officeSprites[198];
                    //Foxy window 19
                    officeSprites[209] = Content.Load<Texture2D>("Office Renders/foxyrun0001");
                    officeSprites[210] = Content.Load<Texture2D>("Office Renders/foxyrun0002");
                    officeSprites[211] = Content.Load<Texture2D>("Office Renders/foxyrun0003");
                    officeSprites[212] = Content.Load<Texture2D>("Office Renders/foxyrun0004");
                    officeSprites[213] = Content.Load<Texture2D>("Office Renders/foxyrun0005");
                    officeSprites[214] = Content.Load<Texture2D>("Office Renders/foxyrun0006");
                    officeSprites[215] = Content.Load<Texture2D>("Office Renders/foxyrun0007");
                    officeSprites[216] = Content.Load<Texture2D>("Office Renders/foxyrun0008");
                    officeSprites[217] = Content.Load<Texture2D>("Office Renders/foxyrun0009");
                    officeSprites[218] = Content.Load<Texture2D>("Office Renders/foxyrun0010");
                    officeSprites[219] = Content.Load<Texture2D>("Office Renders/foxyrun0011");
                    break;
                case 4:
                    //Cheesestick Center Window 20
                    officeSprites[220] = Content.Load<Texture2D>("Office Renders/center cheesestick");
                    officeSprites[221] = officeSprites[220];
                    officeSprites[222] = officeSprites[220];
                    officeSprites[223] = officeSprites[220];
                    officeSprites[224] = officeSprites[220];
                    officeSprites[225] = officeSprites[220];
                    officeSprites[226] = officeSprites[220];
                    officeSprites[227] = officeSprites[220];
                    officeSprites[228] = officeSprites[220];
                    officeSprites[229] = officeSprites[220];
                    officeSprites[230] = officeSprites[220];
                    //Cheesestick Left 21
                    officeSprites[231] = Content.Load<Texture2D>("Office Renders/left cheesestick");
                    officeSprites[232] = officeSprites[231];
                    officeSprites[233] = officeSprites[231];
                    officeSprites[234] = officeSprites[231];
                    officeSprites[235] = officeSprites[231];
                    officeSprites[236] = officeSprites[231];
                    officeSprites[237] = officeSprites[231];
                    officeSprites[238] = officeSprites[231];
                    officeSprites[239] = officeSprites[231];
                    officeSprites[240] = officeSprites[231];
                    officeSprites[241] = officeSprites[231];
                    //Cheesestick Right 22
                    officeSprites[242] = Content.Load<Texture2D>("Office Renders/right cheesestick");
                    officeSprites[243] = officeSprites[242];
                    officeSprites[244] = officeSprites[242];
                    officeSprites[245] = officeSprites[242];
                    officeSprites[246] = officeSprites[242];
                    officeSprites[247] = officeSprites[242];
                    officeSprites[248] = officeSprites[242];
                    officeSprites[249] = officeSprites[242];
                    officeSprites[250] = officeSprites[242];
                    officeSprites[251] = officeSprites[242];
                    officeSprites[252] = officeSprites[242];
                    break;
                case 5:
                    //Jumpscares
                    jumpscareSprites = new Texture2D[247];
                    //Office
                    jumpscareSprites[0] = Content.Load<Texture2D>("Jumpscares/Office/jumpscare0001");
                    jumpscareSprites[1] = Content.Load<Texture2D>("Jumpscares/Office/jumpscare0002");
                    jumpscareSprites[2] = Content.Load<Texture2D>("Jumpscares/Office/jumpscare0003");
                    jumpscareSprites[3] = Content.Load<Texture2D>("Jumpscares/Office/jumpscare0004");
                    jumpscareSprites[4] = Content.Load<Texture2D>("Jumpscares/Office/jumpscare0005");
                    jumpscareSprites[5] = Content.Load<Texture2D>("Jumpscares/Office/jumpscare0006");
                    jumpscareSprites[6] = Content.Load<Texture2D>("Jumpscares/Office/jumpscare0007");
                    jumpscareSprites[7] = Content.Load<Texture2D>("Jumpscares/Office/jumpscare0008");
                    jumpscareSprites[8] = Content.Load<Texture2D>("Jumpscares/Office/jumpscare0009");
                    jumpscareSprites[9] = Content.Load<Texture2D>("Jumpscares/Office/jumpscare0010");
                    jumpscareSprites[10] = Content.Load<Texture2D>("Jumpscares/Office/jumpscare0011");
                    jumpscareSprites[11] = Content.Load<Texture2D>("Jumpscares/Office/jumpscare0012");
                    jumpscareSprites[12] = Content.Load<Texture2D>("Jumpscares/Office/jumpscare0013");
                    jumpscareSprites[13] = Content.Load<Texture2D>("Jumpscares/Office/jumpscare0014");
                    jumpscareSprites[14] = Content.Load<Texture2D>("Jumpscares/Office/jumpscare0015");
                    jumpscareSprites[15] = Content.Load<Texture2D>("Jumpscares/Office/jumpscare0016");
                    jumpscareSprites[16] = Content.Load<Texture2D>("Jumpscares/Office/jumpscare0017");
                    jumpscareSprites[17] = Content.Load<Texture2D>("Jumpscares/Office/jumpscare0018");
                    jumpscareSprites[18] = Content.Load<Texture2D>("Jumpscares/Office/jumpscare0019");
                    jumpscareSprites[19] = Content.Load<Texture2D>("Jumpscares/Office/jumpscare0020");
                    jumpscareSprites[20] = Content.Load<Texture2D>("Jumpscares/Office/jumpscare0021");
                    jumpscareSprites[21] = Content.Load<Texture2D>("Jumpscares/Office/jumpscare0022");
                    jumpscareSprites[22] = Content.Load<Texture2D>("Jumpscares/Office/jumpscare0023");
                    jumpscareSprites[23] = Content.Load<Texture2D>("Jumpscares/Office/jumpscare0024");
                    jumpscareSprites[24] = Content.Load<Texture2D>("Jumpscares/Office/jumpscare0025");
                    jumpscareSprites[25] = Content.Load<Texture2D>("Jumpscares/Office/jumpscare0026");
                    jumpscareSprites[26] = Content.Load<Texture2D>("Jumpscares/Office/jumpscare0027");
                    jumpscareSprites[27] = Content.Load<Texture2D>("Jumpscares/Office/jumpscare0028");
                    jumpscareSprites[28] = Content.Load<Texture2D>("Jumpscares/Office/jumpscare0029");
                    jumpscareSprites[29] = Content.Load<Texture2D>("Jumpscares/Office/jumpscare0030");
                    jumpscareSprites[30] = Content.Load<Texture2D>("Jumpscares/Office/jumpscare0031");
                    jumpscareSprites[31] = Content.Load<Texture2D>("Jumpscares/Office/jumpscare0032");
                    jumpscareSprites[32] = Content.Load<Texture2D>("Jumpscares/Office/jumpscare0033");
                    jumpscareSprites[33] = Content.Load<Texture2D>("Jumpscares/Office/jumpscare0034");
                    jumpscareSprites[34] = Content.Load<Texture2D>("Jumpscares/Office/jumpscare0035");
                    jumpscareSprites[35] = Content.Load<Texture2D>("Jumpscares/Office/jumpscare0036");
                    jumpscareSprites[36] = Content.Load<Texture2D>("Jumpscares/Office/jumpscare0037");
                    jumpscareSprites[37] = Content.Load<Texture2D>("Jumpscares/Office/jumpscare0038");
                    jumpscareSprites[38] = Content.Load<Texture2D>("Jumpscares/Office/jumpscare0039");
                    jumpscareSprites[39] = Content.Load<Texture2D>("Jumpscares/Office/jumpscare0040");
                    jumpscareSprites[40] = Content.Load<Texture2D>("Jumpscares/Office/jumpscare0041");
                    break;
                case 6:
                    //Bonnie
                    jumpscareSprites[41] = Content.Load<Texture2D>("Jumpscares/Bonnie/0000");
                    jumpscareSprites[42] = Content.Load<Texture2D>("Jumpscares/Bonnie/0001");
                    jumpscareSprites[43] = Content.Load<Texture2D>("Jumpscares/Bonnie/0002");
                    jumpscareSprites[44] = Content.Load<Texture2D>("Jumpscares/Bonnie/0003");
                    jumpscareSprites[45] = Content.Load<Texture2D>("Jumpscares/Bonnie/0004");
                    jumpscareSprites[46] = Content.Load<Texture2D>("Jumpscares/Bonnie/0005");
                    jumpscareSprites[47] = Content.Load<Texture2D>("Jumpscares/Bonnie/0006");
                    jumpscareSprites[48] = Content.Load<Texture2D>("Jumpscares/Bonnie/0007");
                    jumpscareSprites[49] = Content.Load<Texture2D>("Jumpscares/Bonnie/0008");
                    jumpscareSprites[50] = Content.Load<Texture2D>("Jumpscares/Bonnie/0009");
                    jumpscareSprites[51] = Content.Load<Texture2D>("Jumpscares/Bonnie/0010");
                    jumpscareSprites[52] = Content.Load<Texture2D>("Jumpscares/Bonnie/0011");
                    jumpscareSprites[53] = Content.Load<Texture2D>("Jumpscares/Bonnie/0012");
                    jumpscareSprites[54] = Content.Load<Texture2D>("Jumpscares/Bonnie/0013");
                    jumpscareSprites[55] = Content.Load<Texture2D>("Jumpscares/Bonnie/0014");
                    jumpscareSprites[56] = Content.Load<Texture2D>("Jumpscares/Bonnie/0015");
                    jumpscareSprites[57] = Content.Load<Texture2D>("Jumpscares/Bonnie/0016");
                    jumpscareSprites[58] = Content.Load<Texture2D>("Jumpscares/Bonnie/0017");
                    jumpscareSprites[59] = Content.Load<Texture2D>("Jumpscares/Bonnie/0018");
                    jumpscareSprites[60] = Content.Load<Texture2D>("Jumpscares/Bonnie/0019");
                    jumpscareSprites[61] = Content.Load<Texture2D>("Jumpscares/Bonnie/0020");
                    jumpscareSprites[62] = Content.Load<Texture2D>("Jumpscares/Bonnie/0021");
                    jumpscareSprites[63] = Content.Load<Texture2D>("Jumpscares/Bonnie/0022");
                    jumpscareSprites[64] = Content.Load<Texture2D>("Jumpscares/Bonnie/0023");
                    jumpscareSprites[65] = Content.Load<Texture2D>("Jumpscares/Bonnie/0024");
                    jumpscareSprites[66] = Content.Load<Texture2D>("Jumpscares/Bonnie/0025");
                    jumpscareSprites[67] = Content.Load<Texture2D>("Jumpscares/Bonnie/0026");
                    jumpscareSprites[68] = Content.Load<Texture2D>("Jumpscares/Bonnie/0027");
                    jumpscareSprites[69] = Content.Load<Texture2D>("Jumpscares/Bonnie/0028");
                    jumpscareSprites[70] = Content.Load<Texture2D>("Jumpscares/Bonnie/0029");
                    jumpscareSprites[71] = Content.Load<Texture2D>("Jumpscares/Bonnie/0030");
                    jumpscareSprites[72] = Content.Load<Texture2D>("Jumpscares/Bonnie/0031");
                    jumpscareSprites[73] = Content.Load<Texture2D>("Jumpscares/Bonnie/0032");
                    jumpscareSprites[74] = Content.Load<Texture2D>("Jumpscares/Bonnie/0033");
                    jumpscareSprites[75] = Content.Load<Texture2D>("Jumpscares/Bonnie/0034");
                    jumpscareSprites[76] = Content.Load<Texture2D>("Jumpscares/Bonnie/0035");
                    jumpscareSprites[77] = Content.Load<Texture2D>("Jumpscares/Bonnie/0036");
                    jumpscareSprites[78] = Content.Load<Texture2D>("Jumpscares/Bonnie/0037");
                    jumpscareSprites[79] = Content.Load<Texture2D>("Jumpscares/Bonnie/0038");
                    jumpscareSprites[80] = Content.Load<Texture2D>("Jumpscares/Bonnie/0039");
                    jumpscareSprites[81] = Content.Load<Texture2D>("Jumpscares/Bonnie/0040");
                    break;
                case 7:
                    //Chica
                    jumpscareSprites[82] = Content.Load<Texture2D>("Jumpscares/Chica/0000");
                    jumpscareSprites[83] = Content.Load<Texture2D>("Jumpscares/Chica/0001");
                    jumpscareSprites[84] = Content.Load<Texture2D>("Jumpscares/Chica/0002");
                    jumpscareSprites[85] = Content.Load<Texture2D>("Jumpscares/Chica/0003");
                    jumpscareSprites[86] = Content.Load<Texture2D>("Jumpscares/Chica/0004");
                    jumpscareSprites[87] = Content.Load<Texture2D>("Jumpscares/Chica/0005");
                    jumpscareSprites[88] = Content.Load<Texture2D>("Jumpscares/Chica/0006");
                    jumpscareSprites[89] = Content.Load<Texture2D>("Jumpscares/Chica/0007");
                    jumpscareSprites[90] = Content.Load<Texture2D>("Jumpscares/Chica/0008");
                    jumpscareSprites[91] = Content.Load<Texture2D>("Jumpscares/Chica/0009");
                    jumpscareSprites[92] = Content.Load<Texture2D>("Jumpscares/Chica/0010");
                    jumpscareSprites[93] = Content.Load<Texture2D>("Jumpscares/Chica/0011");
                    jumpscareSprites[94] = Content.Load<Texture2D>("Jumpscares/Chica/0012");
                    jumpscareSprites[95] = Content.Load<Texture2D>("Jumpscares/Chica/0013");
                    jumpscareSprites[96] = Content.Load<Texture2D>("Jumpscares/Chica/0014");
                    jumpscareSprites[97] = Content.Load<Texture2D>("Jumpscares/Chica/0015");
                    jumpscareSprites[98] = Content.Load<Texture2D>("Jumpscares/Chica/0016");
                    jumpscareSprites[99] = Content.Load<Texture2D>("Jumpscares/Chica/0017");
                    jumpscareSprites[100] = Content.Load<Texture2D>("Jumpscares/Chica/0018");
                    jumpscareSprites[101] = Content.Load<Texture2D>("Jumpscares/Chica/0019");
                    jumpscareSprites[102] = Content.Load<Texture2D>("Jumpscares/Chica/0020");
                    jumpscareSprites[103] = Content.Load<Texture2D>("Jumpscares/Chica/0021");
                    jumpscareSprites[104] = Content.Load<Texture2D>("Jumpscares/Chica/0022");
                    jumpscareSprites[105] = Content.Load<Texture2D>("Jumpscares/Chica/0023");
                    jumpscareSprites[106] = Content.Load<Texture2D>("Jumpscares/Chica/0024");
                    jumpscareSprites[107] = Content.Load<Texture2D>("Jumpscares/Chica/0025");
                    jumpscareSprites[108] = Content.Load<Texture2D>("Jumpscares/Chica/0026");
                    jumpscareSprites[109] = Content.Load<Texture2D>("Jumpscares/Chica/0027");
                    jumpscareSprites[110] = Content.Load<Texture2D>("Jumpscares/Chica/0028");
                    jumpscareSprites[111] = Content.Load<Texture2D>("Jumpscares/Chica/0029");
                    jumpscareSprites[112] = Content.Load<Texture2D>("Jumpscares/Chica/0030");
                    jumpscareSprites[113] = Content.Load<Texture2D>("Jumpscares/Chica/0031");
                    jumpscareSprites[114] = Content.Load<Texture2D>("Jumpscares/Chica/0032");
                    jumpscareSprites[115] = Content.Load<Texture2D>("Jumpscares/Chica/0033");
                    jumpscareSprites[116] = Content.Load<Texture2D>("Jumpscares/Chica/0034");
                    jumpscareSprites[117] = Content.Load<Texture2D>("Jumpscares/Chica/0035");
                    jumpscareSprites[118] = Content.Load<Texture2D>("Jumpscares/Chica/0036");
                    jumpscareSprites[119] = Content.Load<Texture2D>("Jumpscares/Chica/0037");
                    jumpscareSprites[120] = Content.Load<Texture2D>("Jumpscares/Chica/0038");
                    jumpscareSprites[121] = Content.Load<Texture2D>("Jumpscares/Chica/0039");
                    jumpscareSprites[122] = Content.Load<Texture2D>("Jumpscares/Chica/0040");
                    break;
                case 8:
                    //Freddy
                    jumpscareSprites[123] = Content.Load<Texture2D>("Jumpscares/Freddy/0000");
                    jumpscareSprites[124] = Content.Load<Texture2D>("Jumpscares/Freddy/0001");
                    jumpscareSprites[125] = Content.Load<Texture2D>("Jumpscares/Freddy/0002");
                    jumpscareSprites[126] = Content.Load<Texture2D>("Jumpscares/Freddy/0003");
                    jumpscareSprites[127] = Content.Load<Texture2D>("Jumpscares/Freddy/0004");
                    jumpscareSprites[128] = Content.Load<Texture2D>("Jumpscares/Freddy/0005");
                    jumpscareSprites[129] = Content.Load<Texture2D>("Jumpscares/Freddy/0006");
                    jumpscareSprites[130] = Content.Load<Texture2D>("Jumpscares/Freddy/0007");
                    jumpscareSprites[131] = Content.Load<Texture2D>("Jumpscares/Freddy/0008");
                    jumpscareSprites[132] = Content.Load<Texture2D>("Jumpscares/Freddy/0009");
                    jumpscareSprites[133] = Content.Load<Texture2D>("Jumpscares/Freddy/0010");
                    jumpscareSprites[134] = Content.Load<Texture2D>("Jumpscares/Freddy/0011");
                    jumpscareSprites[135] = Content.Load<Texture2D>("Jumpscares/Freddy/0012");
                    jumpscareSprites[136] = Content.Load<Texture2D>("Jumpscares/Freddy/0013");
                    jumpscareSprites[137] = Content.Load<Texture2D>("Jumpscares/Freddy/0014");
                    jumpscareSprites[138] = Content.Load<Texture2D>("Jumpscares/Freddy/0015");
                    jumpscareSprites[139] = Content.Load<Texture2D>("Jumpscares/Freddy/0016");
                    jumpscareSprites[140] = Content.Load<Texture2D>("Jumpscares/Freddy/0017");
                    jumpscareSprites[141] = Content.Load<Texture2D>("Jumpscares/Freddy/0018");
                    jumpscareSprites[142] = Content.Load<Texture2D>("Jumpscares/Freddy/0019");
                    jumpscareSprites[143] = Content.Load<Texture2D>("Jumpscares/Freddy/0020");
                    jumpscareSprites[144] = Content.Load<Texture2D>("Jumpscares/Freddy/0021");
                    jumpscareSprites[145] = Content.Load<Texture2D>("Jumpscares/Freddy/0022");
                    jumpscareSprites[146] = Content.Load<Texture2D>("Jumpscares/Freddy/0023");
                    jumpscareSprites[147] = Content.Load<Texture2D>("Jumpscares/Freddy/0024");
                    jumpscareSprites[148] = Content.Load<Texture2D>("Jumpscares/Freddy/0025");
                    jumpscareSprites[149] = Content.Load<Texture2D>("Jumpscares/Freddy/0026");
                    jumpscareSprites[150] = Content.Load<Texture2D>("Jumpscares/Freddy/0027");
                    jumpscareSprites[151] = Content.Load<Texture2D>("Jumpscares/Freddy/0028");
                    jumpscareSprites[152] = Content.Load<Texture2D>("Jumpscares/Freddy/0029");
                    jumpscareSprites[153] = Content.Load<Texture2D>("Jumpscares/Freddy/0030");
                    jumpscareSprites[154] = Content.Load<Texture2D>("Jumpscares/Freddy/0031");
                    jumpscareSprites[155] = Content.Load<Texture2D>("Jumpscares/Freddy/0032");
                    jumpscareSprites[156] = Content.Load<Texture2D>("Jumpscares/Freddy/0033");
                    jumpscareSprites[157] = Content.Load<Texture2D>("Jumpscares/Freddy/0034");
                    jumpscareSprites[158] = Content.Load<Texture2D>("Jumpscares/Freddy/0035");
                    jumpscareSprites[159] = Content.Load<Texture2D>("Jumpscares/Freddy/0036");
                    jumpscareSprites[160] = Content.Load<Texture2D>("Jumpscares/Freddy/0037");
                    jumpscareSprites[161] = Content.Load<Texture2D>("Jumpscares/Freddy/0038");
                    jumpscareSprites[162] = Content.Load<Texture2D>("Jumpscares/Freddy/0039");
                    jumpscareSprites[163] = Content.Load<Texture2D>("Jumpscares/Freddy/0040");
                    break;
                case 9:
                    //Foxy
                    jumpscareSprites[164] = Content.Load<Texture2D>("Jumpscares/Foxy/0000");
                    jumpscareSprites[165] = Content.Load<Texture2D>("Jumpscares/Foxy/0001");
                    jumpscareSprites[166] = Content.Load<Texture2D>("Jumpscares/Foxy/0002");
                    jumpscareSprites[167] = Content.Load<Texture2D>("Jumpscares/Foxy/0003");
                    jumpscareSprites[168] = Content.Load<Texture2D>("Jumpscares/Foxy/0004");
                    jumpscareSprites[169] = Content.Load<Texture2D>("Jumpscares/Foxy/0005");
                    jumpscareSprites[170] = Content.Load<Texture2D>("Jumpscares/Foxy/0006");
                    jumpscareSprites[171] = Content.Load<Texture2D>("Jumpscares/Foxy/0007");
                    jumpscareSprites[172] = Content.Load<Texture2D>("Jumpscares/Foxy/0008");
                    jumpscareSprites[173] = Content.Load<Texture2D>("Jumpscares/Foxy/0009");
                    jumpscareSprites[174] = Content.Load<Texture2D>("Jumpscares/Foxy/0010");
                    jumpscareSprites[175] = Content.Load<Texture2D>("Jumpscares/Foxy/0011");
                    jumpscareSprites[176] = Content.Load<Texture2D>("Jumpscares/Foxy/0012");
                    jumpscareSprites[177] = Content.Load<Texture2D>("Jumpscares/Foxy/0013");
                    jumpscareSprites[178] = Content.Load<Texture2D>("Jumpscares/Foxy/0014");
                    jumpscareSprites[179] = Content.Load<Texture2D>("Jumpscares/Foxy/0015");
                    jumpscareSprites[180] = Content.Load<Texture2D>("Jumpscares/Foxy/0016");
                    jumpscareSprites[181] = Content.Load<Texture2D>("Jumpscares/Foxy/0017");
                    jumpscareSprites[182] = Content.Load<Texture2D>("Jumpscares/Foxy/0018");
                    jumpscareSprites[183] = Content.Load<Texture2D>("Jumpscares/Foxy/0019");
                    jumpscareSprites[184] = Content.Load<Texture2D>("Jumpscares/Foxy/0020");
                    jumpscareSprites[185] = Content.Load<Texture2D>("Jumpscares/Foxy/0021");
                    jumpscareSprites[186] = Content.Load<Texture2D>("Jumpscares/Foxy/0022");
                    jumpscareSprites[187] = Content.Load<Texture2D>("Jumpscares/Foxy/0023");
                    jumpscareSprites[188] = Content.Load<Texture2D>("Jumpscares/Foxy/0024");
                    jumpscareSprites[189] = Content.Load<Texture2D>("Jumpscares/Foxy/0025");
                    jumpscareSprites[190] = Content.Load<Texture2D>("Jumpscares/Foxy/0026");
                    jumpscareSprites[191] = Content.Load<Texture2D>("Jumpscares/Foxy/0027");
                    jumpscareSprites[192] = Content.Load<Texture2D>("Jumpscares/Foxy/0028");
                    jumpscareSprites[193] = Content.Load<Texture2D>("Jumpscares/Foxy/0029");
                    jumpscareSprites[194] = Content.Load<Texture2D>("Jumpscares/Foxy/0030");
                    jumpscareSprites[195] = Content.Load<Texture2D>("Jumpscares/Foxy/0031");
                    jumpscareSprites[196] = Content.Load<Texture2D>("Jumpscares/Foxy/0032");
                    jumpscareSprites[197] = Content.Load<Texture2D>("Jumpscares/Foxy/0033");
                    jumpscareSprites[198] = Content.Load<Texture2D>("Jumpscares/Foxy/0034");
                    jumpscareSprites[199] = Content.Load<Texture2D>("Jumpscares/Foxy/0035");
                    jumpscareSprites[200] = Content.Load<Texture2D>("Jumpscares/Foxy/0036");
                    jumpscareSprites[201] = Content.Load<Texture2D>("Jumpscares/Foxy/0037");
                    jumpscareSprites[202] = Content.Load<Texture2D>("Jumpscares/Foxy/0038");
                    jumpscareSprites[203] = Content.Load<Texture2D>("Jumpscares/Foxy/0039");
                    jumpscareSprites[204] = Content.Load<Texture2D>("Jumpscares/Foxy/0040");
                    break;
                case 10:
                    //Cheesestick
                    jumpscareSprites[205] = Content.Load<Texture2D>("Jumpscares/Cheesestick/0000");
                    jumpscareSprites[206] = Content.Load<Texture2D>("Jumpscares/Cheesestick/0001");
                    jumpscareSprites[207] = Content.Load<Texture2D>("Jumpscares/Cheesestick/0002");
                    jumpscareSprites[208] = Content.Load<Texture2D>("Jumpscares/Cheesestick/0003");
                    jumpscareSprites[209] = Content.Load<Texture2D>("Jumpscares/Cheesestick/0004");
                    jumpscareSprites[210] = Content.Load<Texture2D>("Jumpscares/Cheesestick/0005");
                    jumpscareSprites[211] = Content.Load<Texture2D>("Jumpscares/Cheesestick/0006");
                    jumpscareSprites[212] = Content.Load<Texture2D>("Jumpscares/Cheesestick/0007");
                    jumpscareSprites[213] = Content.Load<Texture2D>("Jumpscares/Cheesestick/0008");
                    jumpscareSprites[214] = Content.Load<Texture2D>("Jumpscares/Cheesestick/0009");
                    jumpscareSprites[215] = Content.Load<Texture2D>("Jumpscares/Cheesestick/0010");
                    jumpscareSprites[216] = Content.Load<Texture2D>("Jumpscares/Cheesestick/0011");
                    jumpscareSprites[217] = Content.Load<Texture2D>("Jumpscares/Cheesestick/0012");
                    jumpscareSprites[218] = Content.Load<Texture2D>("Jumpscares/Cheesestick/0013");
                    jumpscareSprites[219] = Content.Load<Texture2D>("Jumpscares/Cheesestick/0014");
                    jumpscareSprites[220] = Content.Load<Texture2D>("Jumpscares/Cheesestick/0015");
                    jumpscareSprites[221] = Content.Load<Texture2D>("Jumpscares/Cheesestick/0016");
                    jumpscareSprites[222] = Content.Load<Texture2D>("Jumpscares/Cheesestick/0017");
                    jumpscareSprites[223] = Content.Load<Texture2D>("Jumpscares/Cheesestick/0018");
                    jumpscareSprites[224] = Content.Load<Texture2D>("Jumpscares/Cheesestick/0019");
                    jumpscareSprites[225] = Content.Load<Texture2D>("Jumpscares/Cheesestick/0020");
                    jumpscareSprites[226] = Content.Load<Texture2D>("Jumpscares/Cheesestick/0021");
                    jumpscareSprites[227] = Content.Load<Texture2D>("Jumpscares/Cheesestick/0022");
                    jumpscareSprites[228] = Content.Load<Texture2D>("Jumpscares/Cheesestick/0023");
                    jumpscareSprites[229] = Content.Load<Texture2D>("Jumpscares/Cheesestick/0024");
                    jumpscareSprites[230] = Content.Load<Texture2D>("Jumpscares/Cheesestick/0025");
                    jumpscareSprites[231] = Content.Load<Texture2D>("Jumpscares/Cheesestick/0026");
                    jumpscareSprites[232] = Content.Load<Texture2D>("Jumpscares/Cheesestick/0027");
                    jumpscareSprites[233] = Content.Load<Texture2D>("Jumpscares/Cheesestick/0028");
                    jumpscareSprites[234] = Content.Load<Texture2D>("Jumpscares/Cheesestick/0029");
                    jumpscareSprites[235] = Content.Load<Texture2D>("Jumpscares/Cheesestick/0030");
                    jumpscareSprites[236] = Content.Load<Texture2D>("Jumpscares/Cheesestick/0031");
                    jumpscareSprites[237] = Content.Load<Texture2D>("Jumpscares/Cheesestick/0032");
                    jumpscareSprites[238] = Content.Load<Texture2D>("Jumpscares/Cheesestick/0033");
                    jumpscareSprites[239] = Content.Load<Texture2D>("Jumpscares/Cheesestick/0034");
                    jumpscareSprites[240] = Content.Load<Texture2D>("Jumpscares/Cheesestick/0035");
                    jumpscareSprites[241] = Content.Load<Texture2D>("Jumpscares/Cheesestick/0036");
                    jumpscareSprites[242] = Content.Load<Texture2D>("Jumpscares/Cheesestick/0037");
                    jumpscareSprites[243] = Content.Load<Texture2D>("Jumpscares/Cheesestick/0038");
                    jumpscareSprites[244] = Content.Load<Texture2D>("Jumpscares/Cheesestick/0039");
                    jumpscareSprites[245] = Content.Load<Texture2D>("Jumpscares/Cheesestick/0040");

                    //Power Out Office
                    jumpscareSprites[246] = Content.Load<Texture2D>("Jumpscares/Office/power out");
                    break;
                case 11:
                    //Cameras - Default first
                    camSprites = new Texture2D[42];
                    camSprites[0] = Content.Load<Texture2D>("Cams/stage");
                    camSprites[1] = Content.Load<Texture2D>("Cams/pirates cove");
                    camSprites[2] = Content.Load<Texture2D>("Cams/food counter");
                    camSprites[3] = Content.Load<Texture2D>("Cams/parts and service");
                    camSprites[4] = Content.Load<Texture2D>("Cams/power room");
                    camSprites[5] = Content.Load<Texture2D>("Cams/party room");
                    camSprites[6] = Content.Load<Texture2D>("Cams/left delivery hall");
                    camSprites[7] = Content.Load<Texture2D>("Cams/parts and storage");
                    camSprites[8] = Content.Load<Texture2D>("Cams/bathrooms");
                    //Animatronics get quirky
                    camSprites[9] = Content.Load<Texture2D>("Cams/stage no bon");
                    camSprites[10] = Content.Load<Texture2D>("Cams/stage no chica");
                    camSprites[11] = Content.Load<Texture2D>("Cams/stage no chica or bon");
                    camSprites[12] = Content.Load<Texture2D>("Cams/stage uh oh");
                    camSprites[13] = Content.Load<Texture2D>("Cams/pirates cove oop");
                    camSprites[14] = Content.Load<Texture2D>("Cams/pirates cove uh oh");
                    camSprites[15] = Content.Load<Texture2D>("Cams/parts and storage Foxy");
                    camSprites[16] = Content.Load<Texture2D>("Cams/food counter chica");
                    camSprites[17] = Content.Load<Texture2D>("Cams/food counter matpat");
                    camSprites[18] = Content.Load<Texture2D>("Cams/no camera"); //parts and service bonnie
                    camSprites[19] = Content.Load<Texture2D>("Cams/power room cheesestick");
                    camSprites[20] = Content.Load<Texture2D>("Cams/party room bonnie");
                    camSprites[21] = Content.Load<Texture2D>("Cams/party room fweddy");
                    camSprites[22] = Content.Load<Texture2D>("Cams/left delivery hall cheesestick");
                    camSprites[23] = Content.Load<Texture2D>("Cams/right delivery hall freddy");
                    camSprites[24] = Content.Load<Texture2D>("Cams/bathrooms bonnie close");
                    camSprites[25] = Content.Load<Texture2D>("Cams/bathrooms bonnie soyjack");
                    camSprites[26] = Content.Load<Texture2D>("Cams/bathrooms feddy");
                    camSprites[27] = null; //Kitchen/Fallback
                    camSprites[29] = Content.Load<Texture2D>("Cams/party room cheesestick");
                    camSprites[30] = Content.Load<Texture2D>("Cams/syowen");
                    camSprites[31] = Content.Load<Texture2D>("Cams/mocha");
                    camSprites[32] = Content.Load<Texture2D>("Cams/brett");
                    camSprites[33] = Content.Load<Texture2D>("Cams/bathrooms alan");
                    //UI
                    camSprites[34] = Content.Load<Texture2D>("Cams/Map");
                    camSprites[35] = Content.Load<Texture2D>("Cams/cambutton off");
                    camSprites[36] = Content.Load<Texture2D>("Cams/cambutton on");
                    camSprites[37] = Content.Load<Texture2D>("Jumpscares/static");
                    camSprites[38] = Content.Load<Texture2D>("Cams/Cam bar");
                    //Oops missed some
                    camSprites[39] = Content.Load<Texture2D>("Cams/stage no freddy");
                    camSprites[40] = Content.Load<Texture2D>("Cams/stage bon only");
                    camSprites[41] = Content.Load<Texture2D>("Cams/stage chica only");
                    break;
                case 12:

                    //Buttons
                    Buttons = Content.Load<Texture2D>("Office Renders/buttonspoweron");

                    //Audio
                    sounds = new SoundEffect[28];
                    sounds[0] = Content.Load<SoundEffect>("Audio/light");
                    sounds[1] = Content.Load<SoundEffect>("Audio/boop");
                    sounds[2] = Content.Load<SoundEffect>("Audio/rare boop");
                    sounds[3] = Content.Load<SoundEffect>("Audio/windowscare");
                    sounds[4] = Content.Load<SoundEffect>("Audio/door");
                    sounds[5] = Content.Load<SoundEffect>("Audio/jumpscare");
                    sounds[6] = Content.Load<SoundEffect>("Audio/shortambient1");
                    sounds[7] = Content.Load<SoundEffect>("Audio/shortambient2");
                    sounds[8] = Content.Load<SoundEffect>("Audio/deep steps");
                    sounds[9] = Content.Load<SoundEffect>("Audio/error");
                    sounds[10] = Content.Load<SoundEffect>("Audio/powerdown");
                    sounds[11] = Content.Load<SoundEffect>("Audio/static");
                    sounds[12] = Content.Load<SoundEffect>("Audio/music box");
                    sounds[13] = Content.Load<SoundEffect>("Audio/camera toggle");
                    sounds[14] = Content.Load<SoundEffect>("Audio/changecam");
                    sounds[15] = Content.Load<SoundEffect>("Audio/cam glitch");
                    sounds[16] = Content.Load<SoundEffect>("Audio/freddy");
                    sounds[17] = Content.Load<SoundEffect>("Audio/robotvoice");
                    sounds[18] = Content.Load<SoundEffect>("Audio/6 am");
                    sounds[19] = Content.Load<SoundEffect>("Audio/yay");
                    sounds[20] = Content.Load<SoundEffect>("Audio/running fast3");
                    sounds[21] = Content.Load<SoundEffect>("Audio/run");
                    sounds[22] = Content.Load<SoundEffect>("Audio/knock2");
                    sounds[23] = Content.Load<SoundEffect>("Audio/cheesestick");
                    sounds[24] = Content.Load<SoundEffect>("Audio/bbg");
                    sounds[25] = Content.Load<SoundEffect>("Audio/ring");
                    sounds[26] = Content.Load<SoundEffect>("Audio/pickup");
                    sounds[27] = Content.Load<SoundEffect>("Audio/OVEN-DRA_7_GEN-HDF18121"); //Kitchen
                    if (night < 6)
                    {
                        call = Content.Load<SoundEffect>("Audio/Voices/call " + night.ToString("0"));
                    }
                    //Channels-
                    //0: Lights, 6 am chime
                    //1: Window Jumpscare, boop, 6 am cheer, cam change
                    //2: Door, jumpscare, cam flip, error
                    //3: Ambient noises, power out
                    //4: Footsteps, foxy banging, freddy chimes
                    //5: Camera glitch
                    //6: Freddy hehehe
                    //7: bbg static, hang up phone
                    //8: Phone
                    //9: Kitchen
                    soundchannel = new SoundEffectInstance[10];
                    //Ambience
                    ambience = new Song[2];
                    // TODO: use this.Content to load your game content here
                    ambience[0] = Content.Load<Song>("Audio/fan");
                    ambience[1] = Content.Load<Song>("Audio/cam ambience");

                    soundchannel[9] = sounds[27].CreateInstance();
                    MediaPlayer.IsRepeating = true;
                    MediaPlayer.Play(ambience[0]);
                    break;
                case 13:
                    loaded = true;
                    Game1.Window.Title = "Cheesenaf";
                    Game1.sneakyLoad = false;
                    break;
            }
        }

        public void Initialize(Game1 game1)
        {
            chunkLoad = 0;
            loaded = false;
            Debug = false;
            transitionFinish = true;
            officex = -500;
            camsx = -500;
            rng = new Random();
            Game1 = game1;
            night = Game1.saveData.Night;
            GetNightLevels(night);
            Game1.ClearColor = Color.Black;
            camTime = rng.Next(1, 17);
            baseCam = new int[13];
            camStates = new int[13];
            baseCam[0] = 12; //Stage Cam
            baseCam[1] = 3; //Parts & Service
            baseCam[2] = 4; //Power room
            baseCam[3] = 2; //Food Counter
            baseCam[4] = 14; //Pirate's Cove
            baseCam[5] = 7; //Parts & Storage
            baseCam[6] = 27; //Kitchen
            baseCam[7] = 5; //Party Room A
            baseCam[8] = 5; //Party Room B (Mirror!!)
            baseCam[9] = 6; //Left delivery hall
            baseCam[10] = 6; //Right delivery hall (Mirror!!)
            baseCam[11] = 8; //Left Bathrooms
            baseCam[12] = 8; //Right Bathrooms (Mirror!!)
            camStates[0] = 0; //Stage Cam
            camStates[1] = 3; //Parts & Service
            camStates[2] = 4; //Power room
            camStates[3] = 2; //Food Counter
            camStates[4] = 1; //Pirate's Cove
            camStates[5] = 7; //Parts & Storage
            camStates[6] = 27; //Kitchen
            camStates[7] = 5; //Party Room A
            camStates[8] = 5; //Party Room B (Mirror!!)
            camStates[9] = 6; //Left delivery hall
            camStates[10] = 6; //Right delivery hall (Mirror!!)
            camStates[11] = 8; //Left Bathrooms
            camStates[12] = 8; //Right Bathrooms (Mirror!!)
            camNames = new string[13];
            camNames[0] = "Stage Cam";
            camNames[1] = "Parts & Service";
            camNames[2] = "Power Room";
            camNames[3] = "Food Counter";
            camNames[4] = "Pirate's Cove";
            camNames[5] = "Parts & Storage";
            camNames[6] = "Kitchen";
            camNames[7] = "Party Room A";
            camNames[8] = "Party Room B";
            camNames[9] = "Left Delivery Hall";
            camNames[10] = "Right Delivery Hall";
            camNames[11] = "Left Restrooms";
            camNames[12] = "Right Restrooms";
        }

        private void GetNightLevels(int Night)
        {
            switch (Night)
            {
                default:
                    BonnieAI = 0;
                    ChicaAI = 0;
                    FreddyAI = 0;
                    FoxyAI = 0;
                    bbgAI = 0;
                    break;
                case 1:
                    BonnieAI = 0;
                    ChicaAI = 0;
                    FreddyAI = 0;
                    FoxyAI = 0;
                    bbgAI = 0;
                    break;
                case 2:
                    BonnieAI = 3;
                    ChicaAI = 1;
                    FreddyAI = 0;
                    FoxyAI = 1;
                    bbgAI = 0;
                    break;
                case 3:
                    BonnieAI = 0;
                    ChicaAI = 5;
                    FreddyAI = 1;
                    FoxyAI = 2;
                    bbgAI = 1;
                    break;
                case 4:
                    BonnieAI = 2;
                    ChicaAI = 4;
                    FreddyAI = 2;
                    FoxyAI = 6;
                    bbgAI = 2;
                    break;
                case 5:
                    BonnieAI = 5;
                    ChicaAI = 7;
                    FreddyAI = 3;
                    FoxyAI = 5;
                    bbgAI = 3;
                    break;
                case 6:
                    BonnieAI = 10;
                    ChicaAI = 12;
                    FreddyAI = 4;
                    FoxyAI = 16;
                    bbgAI = 5;
                    break;
                case 7:
                    BonnieAI = Game1.BonnieLevel;
                    ChicaAI = Game1.ChicaLevel;
                    FreddyAI = Game1.FreddyLevel;
                    FoxyAI = Game1.FoxyLevel;
                    bbgAI = (BonnieAI + ChicaAI + FreddyAI + FoxyAI) / 4f;
                    //Trace.WriteLine(bbgAI);
                    if (FreddyAI == 1 && BonnieAI == 7 && ChicaAI == 9 && FoxyAI == 2)
                    {
                        FreddyAI = 0;
                        BonnieAI = 0;
                        ChicaAI = 0;
                        FoxyAI = 0;
                        CheeseAI = 18;
                    }
                    break;
            }
        }

        public void Update(GameTime gameTime)
        {
            mouseState = Game1.mouseState;
            MouseX = Game1.MouseX;
            MouseY = Game1.MouseY;
            if (Game1.GetKeyUp(Keys.Escape))
            {
                Game1.ChangeScene(2);
            }
            if (CheeseAI > 0 && CheeseSound == false)
            {
                soundchannel[3] = sounds[23].CreateInstance();
                soundchannel[3].Volume = 0.25f;
                soundchannel[3].Play();
                CheeseSound = true;
            }
            if (time / 70 < 6 && power > 0)
            {
                //Phone guy
                if (night < 6)
                {
                    if (!phoneAnswered)
                    {
                        if (phoneRingTimer > 0)
                        {
                            phoneRingTimer -= Game1.delta;
                        }
                        else
                        {
                            soundchannel[8] = sounds[25].CreateInstance();
                            soundchannel[8].Play();
                            phoneRingTimer = 5;
                        }
                    }
                    else if (secondphonebool == false)
                    {
                        if (soundchannel[8] != null)
                        {
                            if (soundchannel[8].State == SoundState.Playing) soundchannel[8].Stop();
                        }
                        soundchannel[8] = sounds[26].CreateInstance();
                        soundchannel[8].Play();
                        secondphonebool = true;
                    }
                    else
                    {
                        phoneDelay -= Game1.delta;
                    }

                    if (phoneDelay <= 0)
                    {
                        soundchannel[8] = call.CreateInstance();
                        soundchannel[8].Play();
                        phoneDelay = 9999999;
                    }

                    if (phoneDelay > 999999 && (Game1.MouseX >= 1522 + officex && Game1.MouseX <= 1650 + officex && Game1.MouseY >= 568 && Game1.MouseY <= 604 && Game1.GetMouseDown()))
                    {
                        if (soundchannel[8].State == SoundState.Playing)
                        {
                            soundchannel[8].Stop();
                            soundchannel[7] = sounds[26].CreateInstance();
                            soundchannel[7].Play();
                        }
                    }

                    if (time > 14.5f || (Game1.MouseX >= 1522 + officex && Game1.MouseX <= 1650 + officex && Game1.MouseY >= 568 && Game1.MouseY <= 604 && Game1.GetMouseDown())) phoneAnswered = true;
                }


                //Ai level up
                if (time/70 > 2 && !updateone && CheeseAI == 0)
                {
                    if (BonnieAI < 20)
                    BonnieAI++;

                    updateone = true;
                }
                if (time/70 > 3 && !updatetwo && CheeseAI == 0)
                {
                    if (BonnieAI < 20)
                    BonnieAI++;
                    if (ChicaAI < 20)
                    ChicaAI++;
                    if (FoxyAI < 20)
                    FoxyAI++;

                    updatetwo = true;
                }
                if (time/70 > 4 && !updatethree)
                {
                    if (night == 6)
                    {
                        BonnieAI = 0;
                        ChicaAI = 0;
                        FoxyAI = 0;
                        FreddyAI = 0;
                        BonniePos = 0;
                        ChicaPos = 0;
                        FoxyPos = 0;
                        FreddyPos = 0;
                        bonnieAtDoor = false;
                        chicaAtDoor = false;
                        FreddyTrigger = false;
                        CheeseAI = 18;
                    }
                    else
                    {
                        if (BonnieAI < 20)
                            BonnieAI++;
                        if (ChicaAI < 20)
                            ChicaAI++;
                        if (FoxyAI < 20)
                            FoxyAI++;
                    }

                    updatethree = true;
                }
                if (inCams && rng.Next(0, 1000) == 0 && frametime >= 60 && !syowen && !mocha && !brett && !alan && brokenCamCooldown == 0)
                {
                    brokenCam = rng.Next(2, 6);
                    brokenCamCooldown = brokenCam * 2.5f;
                    soundchannel[5] = sounds[15].CreateInstance();
                    soundchannel[5].Play();
                    frametime = 0;
                }
                else
                if (rng.Next(0, 7500) == 0 && frametime >= 60 && CheeseAI == 0)
                {
                    frametime = 0;
                    if (rng.Next(0, 2) == 0)
                    {
                        soundchannel[3] = sounds[6].CreateInstance();
                        soundchannel[3].Volume = 0.25f;
                        soundchannel[3].Play();
                    }
                    else if (rng.Next(0, 2) == 0)
                    {
                        soundchannel[3] = sounds[7].CreateInstance();
                        soundchannel[3].Volume = 0.25f;
                        soundchannel[3].Play();
                    }
                    else
                    {
                        soundchannel[3] = sounds[17].CreateInstance();
                        soundchannel[3].Volume = 0.25f;
                        soundchannel[3].Play();
                    }
                }
            }

            frametime++;
            if (brokenCam > 0)
            {
                brokenCam -= Game1.delta;
            }
            else
            {
                brokenCam = 0;
                if (soundchannel[5] != null)
                soundchannel[5].Stop();
            }
            if (brokenCamCooldown > 0)
            {
                brokenCamCooldown -= Game1.delta;
            }
            else
            {
                brokenCamCooldown = 0;
            }

                usage = 1;
            if (leftDoor) usage++;
            if (rightDoor) usage++;
            if (leftLight) usage++;
            if (rightLight) usage++;
            if (centerLight) usage++;
            if (inCams) usage++;

            if ((gameTime.ElapsedGameTime.TotalSeconds > 0) && (time / 70 < 6))
            {
                time += Game1.delta;
                if (power > 0)
                    power -= Game1.delta / 8 * usage;
            }

            if (power <= 0 && !powerOutBegin)
            {
                isJumpScare = false;
                freddyPowerTimer = rng.Next(1,21);
                inCams = false;
                powerOutBegin = true;
                MediaPlayer.Stop();
                soundchannel[3] = sounds[10].CreateInstance();
                soundchannel[3].Play();
                BonnieAI = 0;
                ChicaAI = 0;
                FreddyAI = 0;
                FoxyAI = 0;
                if (leftDoor)
                {
                    if (soundToggle == false)
                    {
                        soundToggle = true;
                        soundchannel[2] = sounds[4].CreateInstance();
                        soundchannel[2].Pan = -0.5f;
                        soundchannel[2].Play();
                    }
                    leftDoor = false;
                }
                if (rightDoor)
                {
                    if (soundToggle == false)
                    {
                        soundToggle = true;
                        soundchannel[2] = sounds[4].CreateInstance();
                        soundchannel[2].Pan = 0.5f;
                        soundchannel[2].Play();
                    }
                    rightDoor = false;
                }
            }
            if (powerOutBegin && time / 70 < 6)
            {
                freddyPowerTimer -= Game1.delta;
            }
            if (powerOutBegin && freddyPowerTimer <= 0)
            {
                freddyPowerPos += 1;
                freddyPowerTimer = rng.Next(1, 21);
                if (freddyPowerPos == 1)
                {
                    soundchannel[4] = sounds[12].CreateInstance();
                    soundchannel[4].Play();
                }
                if (freddyPowerPos == 3)
                {
                    FreddyJumpscare();
                }
            }
            if (powerOutBegin && freddyPowerPos == 2)
            {
                soundchannel[4].Stop();
            }
            
            //6 AM
            if (time / 70 >= 6)
            {
                clockAnimOffset -= Game1.delta * 10;
                clockAnimOffset = Math.Clamp(clockAnimOffset, 490, 540);
                isJumpScare = false;
                BonnieAI = 0;
                ChicaAI = 0;
                FreddyAI = 0;
                FoxyAI = 0;
                CheeseAI = 0;
                MediaPlayer.Stop();
                if (!stopSound)
                {
                    int index = 0;
                    foreach (SoundEffectInstance sound in soundchannel)
                    {
                        if (soundchannel[index] != null)
                            soundchannel[index].Stop(); //stop ambient sound
                        index++;
                    }
                    stopSound = true;
                }
                if (!Victory)
                {
                    if (night < 5)
                    {
                        Game1.saveData.Night++;
                        Game1.Save(Game1.saveData);
                    }
                    if (night == 5)
                    {
                        Game1.saveData.SixUnlocked = true;
                        Game1.Save(Game1.saveData);
                    }
                    if (night == 6)
                    {
                        Game1.saveData.CustomUnlocked = true;
                        Game1.saveData.Night = 5;
                        Game1.Save(Game1.saveData);
                    }
                    soundchannel[1] = sounds[19].CreateInstance();
                    soundchannel[0] = sounds[18].CreateInstance();
                    soundchannel[0].Play();
                    Victory = true;
                }
                if (victoryCheerTime >= 0)
                {
                    victoryCheerTime -= Game1.delta;
                    //Trace.WriteLine(victoryCheerTime);
                    if (victoryCheerTime > 1000 && victoryCheerTime < 9995)
                    {
                        if (night < 5)
                            Game1.passStartNight = true;
                        if (night == 6)
                            Game1.passNightSixWin = true;
                        Game1.ChangeScene(2);
                    }
                }
                else
                {
                    victoryCheerTime = 10000;
                    if (night != 6)
                    {
                        soundchannel[1].Play();
                    }
                }
            }
            else
            {

                // TODO: Add your update logic here
                if (!isJumpScare)
                {
                    if (framecounter >= 11)
                    {
                        if (transition == false && transitionFinish == false)
                        {
                            transitionFinish = true;
                        }
                        framecounter = 0;
                        if (transition == true && transitionFinish == false)
                        {
                            transition = false;
                            multiplier = nextMultiplier;
                        }
                    }
                    if (framecounter < 11) framecounter++;

                    if (soundToggle == false && soundchannel[0] != null)
                    {
                        soundchannel[0].Stop();
                    }
                    if (transitionFinish == true)
                    {
                        multiplier = 0;
                    }
                    leftLight = false;
                    rightLight = false;
                    centerLight = false;
                    if (!inCams)
                    {
                        if (power > 0)
                        {
                            //Left Light
                            if (MouseX <= 115 + officex && MouseY >= 608 && MouseY <= 701 && mouseState.LeftButton == ButtonState.Pressed)
                            {
                                leftLight = true;
                                if (bonnieAtDoor == false && cheeseLeftDoor == false)
                                {
                                    if (leftDoor == false)
                                    {
                                        multiplier = 1;
                                    }
                                    else
                                    {
                                        multiplier = 7;
                                    }
                                }
                                else
                                {
                                    if (bonnieAtDoor)
                                    {
                                        if (leftDoor == false)
                                        {
                                            multiplier = 4;
                                        }
                                        else
                                        {
                                            multiplier = 8;
                                        }
                                        if (soundToggle == false)
                                        {
                                            if (seenBonnie == false)
                                            {
                                                soundchannel[1] = sounds[3].CreateInstance();
                                                soundchannel[1].Play();
                                                seenBonnie = true;
                                            }
                                        }
                                    }
                                    else
                                    {
                                        if (leftDoor == false)
                                        {
                                            multiplier = 21;
                                        }
                                        else
                                        {
                                            multiplier = 7;
                                        }
                                        if (soundToggle == false)
                                        {
                                            if (seenCheese == false)
                                            {
                                                soundchannel[1] = sounds[3].CreateInstance();
                                                soundchannel[1].Play();
                                                seenCheese = true;
                                            }
                                        }
                                    }
                                }
                            }
                            //Right Light
                            if (MouseX >= 2754 + officex && MouseY >= 608 && MouseY <= 701 && mouseState.LeftButton == ButtonState.Pressed)
                            {
                                rightLight = true;
                                if (chicaAtDoor == false && cheeseRightDoor == false)
                                {
                                    if (rightDoor == false)
                                    {
                                        multiplier = 2;
                                    }
                                    else
                                    {
                                        multiplier = 16;
                                    }
                                }
                                else
                                {
                                    if (chicaAtDoor)
                                    {
                                        if (rightDoor == false)
                                        {
                                            multiplier = 11;
                                        }
                                        else
                                        {
                                            multiplier = 12;
                                        }
                                        if (soundToggle == false)
                                        {
                                            if (seenChica == false)
                                            {
                                                soundchannel[1] = sounds[3].CreateInstance();
                                                soundchannel[1].Play();
                                                seenChica = true;
                                            }
                                        }
                                    }
                                    else
                                    {
                                        if (rightDoor == false)
                                        {
                                            multiplier = 22;
                                        }
                                        else
                                        {
                                            multiplier = 16;
                                        }
                                        if (soundToggle == false)
                                        {
                                            if (seenCheese == false)
                                            {
                                                soundchannel[1] = sounds[3].CreateInstance();
                                                soundchannel[1].Play();
                                                seenCheese = true;
                                            }
                                        }
                                    }
                                }
                            }
                            //Center Light
                            if (MouseX >= 1415 + officex && MouseX <= 1465 + officex && MouseY >= 177 && MouseY <= 225 && mouseState.LeftButton == ButtonState.Pressed)
                            {
                                centerLight = true;
                                if (ChicaPos == 3 && FreddyPos != 4)
                                    multiplier = 10;
                                else if (ChicaPos == 3 && FreddyPos == 4)
                                    multiplier = 18;
                                else if (ChicaPos != 3 && FreddyPos == 4)
                                    multiplier = 17;
                                else
                                    multiplier = 3;

                                if (FoxyPos == 3)
                                {
                                    if (FoxyTimer > 1.5f)
                                    {
                                        FoxyTimer = 1.5f;
                                        soundchannel[4] = sounds[21].CreateInstance();
                                        soundchannel[4].Pan = -0.15f;
                                        soundchannel[4].Play();
                                    }
                                    multiplier = 19;
                                }
                                if (CheesePos == 4)
                                {
                                    multiplier = 20;
                                }
                            }
                            if (!leftLight && !rightLight && !centerLight)
                            {
                                if (soundchannel[0] != null) soundchannel[0].Stop();
                                soundToggle = false;
                            }
                            else
                            {
                                if (soundToggle == false)
                                {
                                    soundToggle = true;
                                    soundchannel[0] = sounds[0].CreateInstance();
                                    soundchannel[0].IsLooped = true;
                                    soundchannel[0].Play();
                                }
                            }
                            //Boop
                            if (MouseX >= 1820 + officex && MouseX <= 1835 + officex && MouseY >= 515 && MouseY <= 525 && Game1.GetMouseDown())
                            {
                                if (rng.Next(0, 50) == 1)
                                {
                                    soundchannel[1] = sounds[2].CreateInstance();
                                }
                                else
                                {
                                    soundchannel[1] = sounds[1].CreateInstance();
                                }
                                soundchannel[1].Play();

                            }

                            //Left door
                            if (MouseX <= 115 + officex && MouseY >= 715 && MouseY <= 808 && Game1.GetMouseDown() && transition == false)
                            {
                                if (rng.Next(0, 18) == 1)
                                {
                                    soundchannel[2] = sounds[9].CreateInstance();
                                    soundchannel[2].Pan = -0.5f;
                                    soundchannel[2].Play();
                                }
                                else
                                {
                                    if (leftDoor == false)
                                    {
                                        nextMultiplier = 5;
                                        transition = true;
                                        transitionFinish = false;
                                        if (soundToggle == false)
                                        {
                                            soundToggle = true;
                                            soundchannel[2] = sounds[4].CreateInstance();
                                            soundchannel[2].Pan = -0.5f;
                                            soundchannel[2].Play();
                                        }
                                        leftDoor = true;
                                    }
                                    else
                                    {
                                        nextMultiplier = 9;
                                        transition = true;
                                        transitionFinish = false;
                                        if (soundToggle == false)
                                        {
                                            soundToggle = true;
                                            soundchannel[2] = sounds[4].CreateInstance();
                                            soundchannel[2].Pan = -0.5f;
                                            soundchannel[2].Play();
                                        }
                                        leftDoor = false;
                                    }
                                }
                            }
                            if (leftDoor && multiplier == 0 && transition == false) multiplier = 6;
                            //Right door
                            if (MouseX >= 2754 + officex && MouseY >= 715 && MouseY <= 808 && Game1.GetMouseDown() && transition == false)
                            {
                                if (rng.Next(0, 18) == 1)
                                {
                                    soundchannel[2] = sounds[9].CreateInstance();
                                    soundchannel[2].Pan = 0.5f;
                                    soundchannel[2].Play();
                                }
                                else
                                {
                                    if (rightDoor == false)
                                    {
                                        nextMultiplier = 13;
                                        transition = true;
                                        transitionFinish = false;
                                        if (soundToggle == false)
                                        {
                                            soundToggle = true;
                                            soundchannel[2] = sounds[4].CreateInstance();
                                            soundchannel[2].Pan = 0.5f;
                                            soundchannel[2].Play();
                                        }
                                        rightDoor = true;
                                    }
                                    else
                                    {
                                        nextMultiplier = 14;
                                        transition = true;
                                        transitionFinish = false;
                                        if (soundToggle == false)
                                        {
                                            soundToggle = true;
                                            soundchannel[2] = sounds[4].CreateInstance();
                                            soundchannel[2].Pan = 0.5f;
                                            soundchannel[2].Play();
                                        }
                                        rightDoor = false;
                                    }
                                }
                            }
                            if (rightDoor && multiplier == 0 && transition == false) multiplier = 15;
                        }
                        else if ((MouseX <= 115 + officex && MouseY >= 608 && MouseY <= 701 && Game1.GetMouseDown()) ||
                            (MouseX >= 2754 + officex && MouseY >= 608 && MouseY <= 701 && Game1.GetMouseDown()) ||
                            (MouseX >= 1415 + officex && MouseX <= 1465 + officex && MouseY >= 177 && MouseY <= 225 && Game1.GetMouseDown()) ||
                            (MouseX <= 115 + officex && MouseY >= 715 && MouseY <= 808 && Game1.GetMouseDown()) ||
                            (MouseX >= 2754 + officex && MouseY >= 715 && MouseY <= 808 && Game1.GetMouseDown())
                            )
                        {
                            soundchannel[2] = sounds[9].CreateInstance();
                            soundchannel[2].Play();
                        }
                    }

                    
                    //Debug Inputs

                    if (Game1.GetKeyDown(Keys.F3) && Game1.isDebugMode) Debug = !Debug;

                    if (Game1.GetKeyDown(Keys.F1) && Game1.isDebugMode)
                    {
                        bonnieAtDoor = true;
                        BonniePos = 5;
                        seenBonnie = false;
                    }
                    if (Game1.GetKeyDown(Keys.F2) && Keyboard.GetState().IsKeyDown(Keys.LeftShift) == false && Game1.isDebugMode)
                    {
                        chicaAtDoor = true;
                        ChicaPos = 4;
                        seenChica = false;
                    }
                    if (Game1.GetKeyDown(Keys.F2) && Keyboard.GetState().IsKeyDown(Keys.LeftShift) == true && Game1.isDebugMode)
                    {
                        FreddyPos = 4;
                    }
                    if (Game1.GetKeyDown(Keys.F4) && Keyboard.GetState().IsKeyDown(Keys.LeftShift) == false && Game1.isDebugMode)
                    {
                        if (FoxyPos < 3) FoxyPos += 1;
                    }
                    if (Game1.GetKeyDown(Keys.F4) && Keyboard.GetState().IsKeyDown(Keys.LeftShift) == true && Game1.isDebugMode)
                    {
                        if (FoxyPos > 0) FoxyPos -= 1;
                    }

                    if (Game1.GetKeyDown(Keys.F5) && Game1.isDebugMode)
                    {
                        BonnieJumpscare();
                        return;
                    }
                    if (Game1.GetKeyDown(Keys.F6) && Game1.isDebugMode)
                    {
                        ChicaJumpscare();
                        return;
                    }
                    if (Game1.GetKeyDown(Keys.F7) && Game1.isDebugMode)
                    {
                        FreddyJumpscare();
                        return;
                    }
                    if (Game1.GetKeyDown(Keys.F8) && Game1.isDebugMode)
                    {
                        FoxyJumpscare();
                        return;
                    }
                    if (Game1.GetKeyDown(Keys.F9) && Game1.isDebugMode)
                    {
                        CheesestickJumpscare();
                        return;
                    }

                    if (Keyboard.GetState().IsKeyDown(Keys.C) && Keyboard.GetState().IsKeyDown(Keys.D) && Game1.GetKeyDown(Keys.OemPlus))
                    {
                        if (Game1.isDebugMode)
                            time = 70 * 6;
                        else
                        {
                            soundchannel[2] = sounds[9].CreateInstance();
                            soundchannel[2].Play();
                        }
                    }

                    if (Game1.GetKeyDown(Keys.OemQuestion) && Game1.isDebugMode)
                    {
                        power /= 2;
                    }

                    //Foxy Camera Timer
                    if (FoxyAI != 0 && !inCams)
                    {
                        if (camTime > 0)
                        {
                            camTime -= Game1.delta;
                        }
                        else
                        {
                            camTime = 0;
                        }
                    }
                    //Foxy Attack Timer
                    if (FoxyPos == 3)
                    {
                        FoxyTimer -= Game1.delta;
                        if (FoxyTimer <= 0 && leftDoor)
                        {
                            FoxyPos = 0;
                            power -= 1 + (6 * FoxyAttacks);
                            FoxyAttacks++;
                            soundchannel[4] = sounds[22].CreateInstance();
                            soundchannel[4].Pan = -0.5f;
                            soundchannel[4].Play();
                        }
                        if (FoxyTimer <= 0 && !leftDoor)
                        {
                            FoxyJumpscare();
                        }
                    }

                    //Animatronics
                    if (AnimatronicTimer < 297)
                    {
                        AnimatronicTimer++;
                    }
                    else
                    {
                        AnimatronicTimer = 0;
                        //Bonnie
                        if (BonnieAI - 1 >= rng.Next(0, 20))
                        {
                            if (BonniePos < 4)
                            {
                                BonniePos++;
                                if (BonniePos > 2)
                                {
                                    soundchannel[4] = sounds[8].CreateInstance();
                                    soundchannel[4].Pan = -1;
                                    if (BonniePos == 3) soundchannel[4].Volume = 0.2f;
                                    soundchannel[4].Play();
                                }
                            }
                            else if (BonniePos == 4)
                            {
                                bonnieAtDoor = true;
                                if (leftLight)
                                {
                                    if (seenBonnie == false)
                                    {
                                        soundchannel[1] = sounds[3].CreateInstance();
                                        soundchannel[1].Play();
                                        seenBonnie = true;
                                    }
                                }
                                BonniePos++;
                                soundchannel[4] = sounds[8].CreateInstance();
                                soundchannel[4].Volume = 0.5f;
                                soundchannel[4].Pan = -1;
                                soundchannel[4].Play();
                            }
                            else if (BonniePos == 5)
                            {
                                if (leftDoor)
                                {
                                    BonniePos = 0;
                                    bonnieAtDoor = false;
                                    seenBonnie = false;
                                    soundchannel[4] = sounds[8].CreateInstance();
                                    soundchannel[4].Volume = 0.5f;
                                    soundchannel[4].Pan = -1;
                                    soundchannel[4].Play();
                                }
                                else
                                    BonnieJumpscare();
                            }
                        }
                        //Chica
                        if (ChicaAI - 1 >= rng.Next(0, 20))
                        {
                            if (ChicaPos < 3)
                            {
                                ChicaPos++;
                                if (ChicaPos == 2)
                                {
                                    soundchannel[4] = sounds[8].CreateInstance();
                                    soundchannel[4].Pan = 1;
                                    soundchannel[4].Volume = 0.2f;
                                    soundchannel[4].Play();
                                }
                                if (ChicaPos == 3)
                                {
                                    soundchannel[4] = sounds[8].CreateInstance();
                                    soundchannel[4].Pan = 0;
                                    soundchannel[4].Volume = 0.35f;
                                    soundchannel[4].Play();
                                }
                            }
                            else if (ChicaPos == 3)
                            {
                                chicaAtDoor = true;
                                if (rightLight)
                                {
                                    if (seenChica == false)
                                    {
                                        soundchannel[1] = sounds[3].CreateInstance();
                                        soundchannel[1].Play();
                                        seenChica = true;
                                    }
                                }
                                ChicaPos++;
                                soundchannel[4] = sounds[8].CreateInstance();
                                soundchannel[4].Volume = 0.5f;
                                soundchannel[4].Pan = 1;
                                soundchannel[4].Play();
                            }
                            else if (ChicaPos == 4)
                            {
                                if (rightDoor)
                                {
                                    ChicaPos = 0;
                                    chicaAtDoor = false;
                                    seenChica = false;
                                    soundchannel[4] = sounds[8].CreateInstance();
                                    soundchannel[4].Volume = 0.5f;
                                    soundchannel[4].Pan = 1;
                                    soundchannel[4].Play();
                                }
                                else
                                    ChicaJumpscare();
                            }
                        }
                        //Freddy
                        if (FreddyAI - 1 >= rng.Next(0, 20) || FreddyTrigger)
                        {
                            if (FreddyPos <= 4)
                            {
                                FreddyPos++;
                                soundchannel[6] = sounds[16].CreateInstance();
                                soundchannel[6].Volume = 0.25f;
                                soundchannel[6].Play();
                            }
                            else if (FreddyPos == 5)
                            {
                                if (rightDoor)
                                {
                                    FreddyPos = 4;
                                    soundchannel[6] = sounds[16].CreateInstance();
                                    soundchannel[6].Volume = 0.25f;
                                    soundchannel[6].Play();
                                    FreddyTrigger = false;
                                }
                                else if (inCams)
                                {
                                    FreddyJumpscare();
                                }
                                else
                                {
                                    FreddyTrigger = true;
                                }
                            }
                        }
                        //Foxy
                        if (FoxyAI - 1 >= rng.Next(0, 20))
                        {
                            if (camTime <= 0 && !inCams)
                            {
                                if (FoxyPos < 2)
                                {
                                    FoxyPos++;
                                }
                                else if (FoxyPos == 2)
                                {
                                    FoxyPos++;
                                    soundchannel[4] = sounds[20].CreateInstance();
                                    soundchannel[4].Volume = 0.25f;
                                    soundchannel[4].Play();
                                    FoxyTimer = 15;
                                }
                            }
                        }
                        //Cheesestick
                        if (CheeseAI - 1 >= rng.Next(0, 20))
                        {
                            if (CheesePos < 4)
                            {
                                CheesePos++;
                            }
                            else if (CheesePos == 4)
                            {
                                if (rng.Next(0,2) == 0)
                                {
                                    cheeseLeftDoor = true;
                                }
                                else
                                {
                                    cheeseRightDoor = true;
                                }
                                CheesePos++;
                            }
                            else if (CheesePos == 5)
                            {
                                if (leftDoor && cheeseLeftDoor)
                                {
                                    CheesePos = 0;
                                    cheeseLeftDoor = false;
                                    seenCheese = false;
                                }
                                else if (cheeseLeftDoor)
                                    CheesestickJumpscare();

                                if (rightDoor && cheeseRightDoor)
                                {
                                    CheesePos = 0;
                                    cheeseRightDoor = false;
                                    seenCheese = false;
                                }
                                else if (cheeseRightDoor)
                                    CheesestickJumpscare();
                            }
                        }
                    }
                    //Cams

                    if (inCams == false)
                    {
                        if (MouseX <= 650 && MouseX > 550) officex += 1;
                        if (MouseX <= 550 && MouseX > 450) officex += 5;
                        if (MouseX <= 450 && MouseX > 350) officex += 10;
                        if (MouseX <= 350 && MouseX > 250) officex += 25;
                        if (MouseX <= 250 && MouseX > 150) officex += 40;
                        if (MouseX <= 150) officex += 50;

                        if (MouseX >= 1250 && MouseX < 1350) officex -= 1;
                        if (MouseX >= 1350 && MouseX < 1450) officex -= 5;
                        if (MouseX >= 1450 && MouseX < 1550) officex -= 10;
                        if (MouseX >= 1550 && MouseX < 1650) officex -= 25;
                        if (MouseX >= 1650 && MouseX < 1750) officex -= 40;
                        if (MouseX >= 1750) officex -= 50;
                    }

                    if (Game1.GetMouseUp()) soundToggle = false;

                    if (ChicaPos == 2)
                    {
                        if (inCams && currentCam == 6)
                        {
                            soundchannel[9].Volume = 0.75f;
                        }
                        else
                        {
                            soundchannel[9].Volume = 0.05f;
                        }
                        if (soundchannel[9].State != SoundState.Playing)
                            soundchannel[9].Play();
                    }
                    else
                    {
                        soundchannel[9].Stop();
                    }

                    officex = Math.Clamp(officex, -950, 0);
                    if (addcamsx)
                    {
                        secretcamsx += 5;
                        if (secretcamsx > 150) addcamsx = false;
                    }
                    else
                    {
                        secretcamsx -= 5;
                        if (secretcamsx < -1050) addcamsx = true;
                    }
                    camsx = secretcamsx;
                    camsx = Math.Clamp(camsx, -950, 0);
                    if (framecounter > 0)
                    {
                        theOffice = officeSprites[framecounter + (11 * multiplier) - 1];
                    }

                    //Stage Cam
                    if (BonniePos == 0 && FreddyPos == 0 && ChicaPos == 0)
                        camStates[0] = 0;
                    if (BonniePos != 0 && ChicaPos == 0)
                        camStates[0] = 9;
                    if (BonniePos == 0 && ChicaPos != 0)
                        camStates[0] = 10;
                    if (BonniePos != 0 && ChicaPos != 0)
                        camStates[0] = 11;
                    if (BonniePos == 0 && FreddyPos != 0 && ChicaPos == 0)
                        camStates[0] = 39;
                    if (BonniePos == 0 && FreddyPos != 0 && ChicaPos != 0)
                        camStates[0] = 40;
                    if (BonniePos != 0 && FreddyPos != 0 && ChicaPos == 0)
                        camStates[0] = 41;
                    if (BonniePos != 0 && ChicaPos != 0 && FreddyPos != 0)
                        camStates[0] = 12;
                    //Parts & Service
                    if (BonniePos != 1)
                        camStates[1] = 3;
                    if (BonniePos == 1)
                        camStates[1] = 18;
                    //Power Room
                    if (CheesePos == 1)
                        camStates[2] = 19;
                    if (CheesePos != 1)
                        camStates[2] = 4;
                    if (brett)
                        camStates[2] = 32;
                    //Food Counter
                    if (ChicaPos != 1)
                        camStates[3] = 2;
                    if (ChicaPos == 1)
                        camStates[3] = 16;
                    if (FreddyPos == 1)
                        camStates[3] = 17;
                    if (syowen)
                        camStates[3] = 30;
                    //Pirate's Cove
                    if (FoxyPos == 0)
                        camStates[4] = 1;
                    if (FoxyPos == 1)
                        camStates[4] = 13;
                    if (FoxyPos >= 2)
                        camStates[4] = 14;
                    if (mocha)
                        camStates[4] = 31;
                    //Parts & Storage
                    if (FoxyPos != 2)
                        camStates[5] = 7;
                    if (FoxyPos == 2)
                        camStates[5] = 15;
                    //Kitchen
                    camStates[6] = 27;
                    //Party Room A
                    if (BonniePos != 2)
                        camStates[7] = 5;
                    if (BonniePos == 2)
                        camStates[7] = 20;
                    if (CheesePos == 2)
                        camStates[7] = 29;
                    //Party Room B (Mirror!!)
                    if (FreddyPos != 2)
                        camStates[8] = 5;
                    if (FreddyPos == 2)
                        camStates[8] = 21;
                    //Left delivery hall
                    if (CheesePos == 3)
                        camStates[9] = 22;
                    if (CheesePos != 3)
                        camStates[9] = 6;
                    //Right delivery hall (Mirror!!)
                    if (FreddyPos != 3)
                        camStates[10] = 6;
                    if (FreddyPos == 3)
                        camStates[10] = 23;
                    //Left Bathrooms
                    if (BonniePos != 3 && BonniePos != 4)
                        camStates[11] = 8; //Left Bathrooms
                    if (BonniePos == 3)
                        camStates[11] = 24;
                    if (BonniePos == 4)
                        camStates[11] = 25;
                    //Right Bathrooms (Mirror!!)
                    if (FreddyPos != 5)
                        camStates[12] = 8;
                    if (FreddyPos == 5)
                        camStates[12] = 26;
                    if (alan)
                        camStates[12] = 33;

                    if (MouseY < 1050 && !bbgTooLate) canCam = true;

                    if (MouseY >= 1050 && canCam && power > 0)
                    {
                        inCams = !inCams;
                        MediaPlayer.IsRepeating = true;
                        if (inCams)
                        {
                            MediaPlayer.Play(ambience[1]);
                            if (brokenCam > 0)
                            {
                                soundchannel[5] = sounds[15].CreateInstance();
                                soundchannel[5].Play();
                            }
                            if (night > 1)
                            {
                                if (Game1.saveData.Bbg == 0)
                                {
                                    syowen = rng.Next(0, 20) <= (bbgAI / 3f) ? true : false;
                                }
                                else
                                {
                                    syowen = rng.Next(0, 20) <= (bbgAI / 4f) ? true : false;
                                }
                                if (Game1.saveData.Bbg == 1)
                                {
                                    mocha = rng.Next(0, 20) <= (bbgAI / 3f) ? true : false;
                                }
                                else
                                {
                                    mocha = rng.Next(0, 20) <= (bbgAI / 4f) ? true : false;
                                }
                                if (Game1.saveData.Bbg == 2)
                                {
                                    brett = rng.Next(0, 20) <= (bbgAI / 3f) ? true : false;
                                }
                                else
                                {
                                    brett = rng.Next(0, 20) <= (bbgAI / 4f) ? true : false;
                                }
                                if (Game1.saveData.Bbg == 3)
                                {
                                    alan = rng.Next(0, 20) <= (bbgAI / 3f) ? true : false;
                                }
                                else
                                {
                                    alan = rng.Next(0, 20) <= (bbgAI / 4f) ? true : false;
                                }
                            }
                        }
                        else
                        {
                            MediaPlayer.Play(ambience[0]);
                            if (soundchannel[5] != null)
                            soundchannel[5].Stop();
                            camTime = rng.Next(2, 17);
                            syowen = false;
                            mocha = false;
                            brett = false;
                            alan = false;
                            bbgLookTime = 0;
                        }
                        canCam = false;
                        soundchannel[2] = sounds[13].CreateInstance();
                        soundchannel[2].Play();
                    }

                    if (inCams)
                    {
                        //Cams ui
                        if (Game1.GetMouseDown() && canCam)
                        {
                            int[] buttonPosX = new int[13] { 1675, 1490, 1530, 1700, 1620, 1620, 1700, 1510, 1810, 1610, 1710, 1530, 1790 };
                            int[] buttonPosY = new int[13] { 545, 615, 670, 750, 750, 805, 805, 865, 865, 885, 885, 980, 980 };
                            int index = 0;
                            int getCam()
                            {
                                while (index < 13)
                                {
                                    if (MouseX >= buttonPosX[index] && MouseX <= buttonPosX[index] + 64)
                                    {
                                        if (MouseY >= buttonPosY[index] && MouseY <= buttonPosY[index] + 32)
                                        {
                                            return index;
                                        }
                                    }
                                    index++;
                                }
                                return 999;
                            }
                            if (getCam() != 999)
                            {
                                currentCam = getCam();
                                soundchannel[1] = sounds[14].CreateInstance();
                                soundchannel[1].Play();
                            }

                        }

                        staticMultiplier++;
                        if (staticMultiplier > 7) staticMultiplier = 0;
                        //BBG jumpscares
                        if (currentCam == 3 && syowen && brokenCam == 0)
                        {
                            bbgLookTime += Game1.delta;
                        }
                        else if (currentCam == 4 && mocha && brokenCam == 0)
                        {
                            bbgLookTime += Game1.delta;
                        }
                        else if (currentCam == 2 && brett && brokenCam == 0)
                        {
                            bbgLookTime += Game1.delta;
                        }
                        else if (currentCam == 12 && alan && brokenCam == 0)
                        {
                            bbgLookTime += Game1.delta;
                        }
                        else
                        {
                            bbgLookTime = 0;
                        }

                        if (bbgTooLate)
                        {
                            canCam = false;
                            bbgLookTime += Game1.delta / 5;
                            if (bbgLookTime >= 1f)
                            {
                                Game1.saveData.AltTitle = false;
                                if (Game1.saveData.Night > 5) Game1.saveData.Night = 5;
                                Game1.Save(Game1.saveData);
                                Game1.ChangeScene(0); //Change to 1 when bbg dating sim exists
                            }
                        }

                        if (bbgLookTime > 0.75f && !bbgTooLate)
                        {
                            canCam = false;
                            soundchannel[7] = sounds[24].CreateInstance();
                            soundchannel[7].Play();
                            bbgTooLate = true;
                            bbgLookTime = 0;
                        }
                    }

                }
                else //Jumpscare
                {
                    inCams = false;
                    officex = Math.Clamp(officex, -950, 0);
                    theOffice = jumpscareSprites[framecounter];
                    Animatronic = jumpscareSprites[framecounter + (41 * jumpscareID)];
                    if (framecounter < 40)
                    {
                        framecounter++;
                    }
                    else
                    {
                        Game1.passJumpscare = true;
                        Game1.ChangeScene(2);
                    }
                }
            }
        }

        public void Draw(SpriteBatch _spriteBatch, GameTime gameTime, SpriteFont defaultfont, SpriteFont pixelfont, Texture2D debugbox)
        {
            if (time / 70 < 6)
            {
                if (power <= 0)
                {
                    if (freddyPowerPos == 1 && rng.Next(0,20) < 17)
                    {
                        _spriteBatch.Draw(jumpscareSprites[246], new Vector2(officex, 0), Color.White);
                    }
                }
                else
                {
                    if (!inCams)
                    {
                        _spriteBatch.Draw(officeSprites[0], new Vector2(officex, 0), Color.White);
                        if (leftLight && !leftDoor)
                            _spriteBatch.Draw(officeSprites[11], new Vector2(officex, 0), Color.White);
                        if (leftDoor && !leftLight && !transition)
                            _spriteBatch.Draw(officeSprites[66], new Vector2(officex, 0), Color.White);
                        if (leftDoor && leftLight && !transition)
                            _spriteBatch.Draw(officeSprites[77], new Vector2(officex, 0), Color.White);
                        if (!rightDoor && rightLight)
                            _spriteBatch.Draw(officeSprites[22], new Vector2(officex, 0), Color.White);
                        if (rightDoor && !rightLight && !transition)
                            _spriteBatch.Draw(officeSprites[165], new Vector2(officex, 0), Color.White);
                        if (rightDoor && rightLight && !transition)
                            _spriteBatch.Draw(officeSprites[176], new Vector2(officex, 0), Color.White);
                        if (isJumpScare == false)
                        {
                            if (framecounter > 0)
                            {
                                _spriteBatch.Draw(officeSprites[framecounter - 1], new Vector2(officex, 0), Color.White);

                                _spriteBatch.Draw(theOffice, new Vector2(officex, 0), Color.White);
                            }
                            else
                            {
                                _spriteBatch.Draw(officeSprites[0], new Vector2(officex, 0), Color.White);
                            }
                            _spriteBatch.Draw(Buttons, new Vector2(officex, 0), Color.White);
                        }
                    }
                    if (inCams)
                    {
                        if (camSprites[camStates[currentCam]] != null && brokenCam <= 0)
                        {
                            if (currentCam == 8 || currentCam == 10 || currentCam == 12)
                            {
                                _spriteBatch.Draw(camSprites[baseCam[currentCam]], new Vector2(camsx, 0), new Rectangle(0, 0, 1920, 720), Color.White, 0f, new Vector2(0, 0), 1.5f, SpriteEffects.FlipHorizontally, 0);
                                _spriteBatch.Draw(camSprites[camStates[currentCam]], new Vector2(camsx, 0), new Rectangle(0, 0, 1920, 720), Color.White, 0f, new Vector2(0, 0), 1.5f, SpriteEffects.FlipHorizontally, 0);
                            }
                            else
                            {
                                _spriteBatch.Draw(camSprites[baseCam[currentCam]], new Vector2(camsx, 0), new Rectangle(0, 0, 1920, 720), Color.White, 0f, new Vector2(0, 0), 1.5f, SpriteEffects.None, 0);
                                _spriteBatch.Draw(camSprites[camStates[currentCam]], new Vector2(camsx, 0), new Rectangle(0, 0, 1920, 720), Color.White, 0f, new Vector2(0, 0), 1.5f, SpriteEffects.None, 0);
                            }
                        }
                        else
                        {
                            _spriteBatch.DrawString(defaultfont, "NO CAMERA DATA", new Vector2(860, 540), Color.White);
                        }
                        if (Game1.showFPS)
                        {
                            _spriteBatch.DrawString(defaultfont, camNames[currentCam], new Vector2(0, 15), Color.White);
                        }
                        else
                        {
                            _spriteBatch.DrawString(defaultfont, camNames[currentCam], new Vector2(0, 0), Color.White);
                        }
                        _spriteBatch.Draw(camSprites[37], new Vector2(0, 0), new Rectangle(0, (staticMultiplier * 720) + 4 + (staticMultiplier * 2), 1280, 720), Color.White * 0.25f, 0, new Vector2(0, 0), 1.5f, SpriteEffects.None, 0);
                        //Map
                        _spriteBatch.Draw(camSprites[34], new Vector2(1500, 550), new Rectangle(0, 0, 128, 149), Color.White, 0, new Vector2(0, 0), 3, SpriteEffects.None, 0);
                        //Stage cam buttons
                        _spriteBatch.Draw(camSprites[35], new Vector2(1675, 545), new Rectangle(0, 0, 128, 64), Color.White, 0, new Vector2(0, 0), 0.5f, SpriteEffects.None, 0); //Stage
                        _spriteBatch.Draw(camSprites[35], new Vector2(1490, 615), new Rectangle(0, 0, 128, 64), Color.White, 0, new Vector2(0, 0), 0.5f, SpriteEffects.None, 0); //PartsService
                        _spriteBatch.Draw(camSprites[35], new Vector2(1530, 670), new Rectangle(0, 0, 128, 64), Color.White, 0, new Vector2(0, 0), 0.5f, SpriteEffects.None, 0); //Power
                        _spriteBatch.Draw(camSprites[35], new Vector2(1700, 750), new Rectangle(0, 0, 128, 64), Color.White, 0, new Vector2(0, 0), 0.5f, SpriteEffects.None, 0); //Food
                        _spriteBatch.Draw(camSprites[35], new Vector2(1620, 750), new Rectangle(0, 0, 128, 64), Color.White, 0, new Vector2(0, 0), 0.5f, SpriteEffects.None, 0); //Piratecove
                        _spriteBatch.Draw(camSprites[35], new Vector2(1620, 805), new Rectangle(0, 0, 128, 64), Color.White, 0, new Vector2(0, 0), 0.5f, SpriteEffects.None, 0); //PartsStorage
                        _spriteBatch.Draw(camSprites[35], new Vector2(1700, 805), new Rectangle(0, 0, 128, 64), Color.White, 0, new Vector2(0, 0), 0.5f, SpriteEffects.None, 0); //Kitchen
                        _spriteBatch.Draw(camSprites[35], new Vector2(1510, 865), new Rectangle(0, 0, 128, 64), Color.White, 0, new Vector2(0, 0), 0.5f, SpriteEffects.None, 0); //PartyA
                        _spriteBatch.Draw(camSprites[35], new Vector2(1810, 865), new Rectangle(0, 0, 128, 64), Color.White, 0, new Vector2(0, 0), 0.5f, SpriteEffects.None, 0); //PartyB
                        _spriteBatch.Draw(camSprites[35], new Vector2(1610, 885), new Rectangle(0, 0, 128, 64), Color.White, 0, new Vector2(0, 0), 0.5f, SpriteEffects.None, 0); //LDHall
                        _spriteBatch.Draw(camSprites[35], new Vector2(1710, 885), new Rectangle(0, 0, 128, 64), Color.White, 0, new Vector2(0, 0), 0.5f, SpriteEffects.None, 0); //RDHall
                        _spriteBatch.Draw(camSprites[35], new Vector2(1530, 980), new Rectangle(0, 0, 128, 64), Color.White, 0, new Vector2(0, 0), 0.5f, SpriteEffects.None, 0); //LBathrooms
                        _spriteBatch.Draw(camSprites[35], new Vector2(1790, 980), new Rectangle(0, 0, 128, 64), Color.White, 0, new Vector2(0, 0), 0.5f, SpriteEffects.None, 0); //RBathrooms
                        switch (currentCam)
                        { //1500 left, 550 top
                            case 0:
                                _spriteBatch.Draw(camSprites[36], new Vector2(1675, 545), new Rectangle(0, 0, 128, 64), Color.White, 0, new Vector2(0, 0), 0.5f, SpriteEffects.None, 0); //Stage
                                break;
                            case 1:
                                _spriteBatch.Draw(camSprites[36], new Vector2(1490, 615), new Rectangle(0, 0, 128, 64), Color.White, 0, new Vector2(0, 0), 0.5f, SpriteEffects.None, 0); //PartsService
                                break;
                            case 2:
                                _spriteBatch.Draw(camSprites[36], new Vector2(1530, 670), new Rectangle(0, 0, 128, 64), Color.White, 0, new Vector2(0, 0), 0.5f, SpriteEffects.None, 0); //Power
                                break;
                            case 3:
                                _spriteBatch.Draw(camSprites[36], new Vector2(1700, 750), new Rectangle(0, 0, 128, 64), Color.White, 0, new Vector2(0, 0), 0.5f, SpriteEffects.None, 0); //Food
                                break;
                            case 4:
                                _spriteBatch.Draw(camSprites[36], new Vector2(1620, 750), new Rectangle(0, 0, 128, 64), Color.White, 0, new Vector2(0, 0), 0.5f, SpriteEffects.None, 0); //Piratecove
                                break;
                            case 5:
                                _spriteBatch.Draw(camSprites[36], new Vector2(1620, 805), new Rectangle(0, 0, 128, 64), Color.White, 0, new Vector2(0, 0), 0.5f, SpriteEffects.None, 0); //PartsStorage
                                break;
                            case 6:
                                _spriteBatch.Draw(camSprites[36], new Vector2(1700, 805), new Rectangle(0, 0, 128, 64), Color.White, 0, new Vector2(0, 0), 0.5f, SpriteEffects.None, 0); //Kitchen
                                break;
                            case 7:
                                _spriteBatch.Draw(camSprites[36], new Vector2(1510, 865), new Rectangle(0, 0, 128, 64), Color.White, 0, new Vector2(0, 0), 0.5f, SpriteEffects.None, 0); //PartyA
                                break;
                            case 8:
                                _spriteBatch.Draw(camSprites[36], new Vector2(1810, 865), new Rectangle(0, 0, 128, 64), Color.White, 0, new Vector2(0, 0), 0.5f, SpriteEffects.None, 0); //PartyB
                                break;
                            case 9:
                                _spriteBatch.Draw(camSprites[36], new Vector2(1610, 885), new Rectangle(0, 0, 128, 64), Color.White, 0, new Vector2(0, 0), 0.5f, SpriteEffects.None, 0); //LDHall
                                break;
                            case 10:
                                _spriteBatch.Draw(camSprites[36], new Vector2(1710, 885), new Rectangle(0, 0, 128, 64), Color.White, 0, new Vector2(0, 0), 0.5f, SpriteEffects.None, 0); //RDHall
                                break;
                            case 11:
                                _spriteBatch.Draw(camSprites[36], new Vector2(1530, 980), new Rectangle(0, 0, 128, 64), Color.White, 0, new Vector2(0, 0), 0.5f, SpriteEffects.None, 0); //LBathrooms
                                break;
                            case 12:
                                _spriteBatch.Draw(camSprites[36], new Vector2(1790, 980), new Rectangle(0, 0, 128, 64), Color.White, 0, new Vector2(0, 0), 0.5f, SpriteEffects.None, 0); //RBathrooms
                                break;
                        }
                        _spriteBatch.DrawString(pixelfont, "CAM 0", new Vector2(1675, 545), Color.White); //Stage
                        _spriteBatch.DrawString(pixelfont, "CAM 1", new Vector2(1490, 615), Color.White); //PartsService
                        _spriteBatch.DrawString(pixelfont, "CAM 2", new Vector2(1530, 670), Color.White); //Power
                        _spriteBatch.DrawString(pixelfont, "CAM 3", new Vector2(1700, 750), Color.White); //Food
                        _spriteBatch.DrawString(pixelfont, "CAM 4", new Vector2(1620, 750), Color.White); //Piratecove
                        _spriteBatch.DrawString(pixelfont, "CAM 5", new Vector2(1620, 805), Color.White); //PartsStorage
                        _spriteBatch.DrawString(pixelfont, "CAM 6", new Vector2(1700, 805), Color.White); //Kitchen
                        _spriteBatch.DrawString(pixelfont, "CAM 7", new Vector2(1510, 865), Color.White); //PartyA
                        _spriteBatch.DrawString(pixelfont, "CAM 8", new Vector2(1810, 865), Color.White); //PartyB
                        _spriteBatch.DrawString(pixelfont, "CAM 9", new Vector2(1610, 885), Color.White); //LDHall
                        _spriteBatch.DrawString(pixelfont, "CAM 10", new Vector2(1710, 885), Color.White); //RDHall
                        _spriteBatch.DrawString(pixelfont, "CAM 11", new Vector2(1530, 980), Color.White); //LBathrooms
                        _spriteBatch.DrawString(pixelfont, "CAM 12", new Vector2(1790, 980), Color.White); //RBathrooms
                        if (bbgTooLate)
                        {
                            _spriteBatch.Draw(camSprites[37], new Vector2(0, 0), new Rectangle(0, (staticMultiplier * 720) + 4 + (staticMultiplier * 2), 1280, 720), Color.White * bbgLookTime, 0, new Vector2(0, 0), 1.5f, SpriteEffects.None, 0);
                        }
                    }

                }
                if (isJumpScare && Animatronic != null)
                {
                    if (power > 0)
                        _spriteBatch.Draw(theOffice, new Vector2(officex, 0), Color.White);
                    _spriteBatch.Draw(Animatronic, new Vector2(0, 0), new Rectangle(0,0,1024,576), Color.White, 0,new Vector2(0,0),1.875f,SpriteEffects.None,0);
                }
                //UI
                if (MathF.Floor(time / 70) < 1)
                    _spriteBatch.DrawString(defaultfont, "12 A.M.", new Vector2(1780, 0), Color.White);
                else
                    _spriteBatch.DrawString(defaultfont, MathF.Floor(time / 70).ToString("0") + "A.M.", new Vector2(1780, 0), Color.White);
                _spriteBatch.DrawString(defaultfont, night.ToString("Night 0"), new Vector2(1780, 50), Color.White);
                _spriteBatch.DrawString(defaultfont, "Power Left: " + MathF.Abs(MathF.Ceiling(power)).ToString() + "%", new Vector2(20, 960), Color.White);
                _spriteBatch.DrawString(defaultfont, "Usage: " + usage.ToString(), new Vector2(20, 1010), Color.White);
                if (canCam)
                {
                    _spriteBatch.Draw(camSprites[38], new Vector2(900, 1065), new Rectangle(0, 0, 64, 9), Color.White, 0, new Vector2(camSprites[38].Width / 2, camSprites[38].Height / 2), new Vector2(18, 3), SpriteEffects.None, 0);
                    _spriteBatch.DrawString(pixelfont, ">>", new Vector2(950, 1090), Color.White, 1.575f, new Vector2(camSprites[38].Width / 2, camSprites[38].Height / 2), new Vector2(1f, 5), SpriteEffects.None, 0);
                }

                //Debug Screen
                if (Debug == true)
                {
                    _spriteBatch.DrawString(pixelfont, MouseX.ToString() + ", " + MouseY.ToString() + " || " + mouseState.X.ToString() + ", " + mouseState.Y.ToString(), new Vector2(0, 50), Color.White);
                    _spriteBatch.DrawString(pixelfont, officex.ToString() + " || " + camsx.ToString() + " || " + secretcamsx.ToString(), new Vector2(0, 100), Color.White);
                    _spriteBatch.DrawString(pixelfont, multiplier.ToString() + "||" + nextMultiplier.ToString() + "||" + soundToggle.ToString(), new Vector2(0, 150), Color.White);
                    _spriteBatch.DrawString(pixelfont, transition.ToString(), new Vector2(0, 200), Color.White);
                    _spriteBatch.DrawString(pixelfont, "[" + AnimatronicTimer.ToString() + "] Bonnie: " + BonnieAI.ToString() + ":" + BonniePos.ToString() + ", Chica: " + ChicaAI.ToString() + ":" + ChicaPos.ToString() + ", Freddy: " + FreddyAI.ToString() + ":" + FreddyPos.ToString() + " (" + FreddyTrigger.ToString() + "), Foxy: " + FoxyAI.ToString() + ":" + FoxyPos.ToString(), new Vector2(0, 250), Color.White);
                    _spriteBatch.DrawString(pixelfont, MathF.Floor(time / 60).ToString() + ":" + (time % 60).ToString("00.00"), new Vector2(0, 300), Color.White);
                    _spriteBatch.DrawString(pixelfont, camTime.ToString() + " || " + FoxyTimer.ToString(), new Vector2(0, 350), Color.White);
                    _spriteBatch.DrawString(pixelfont, syowen.ToString() + " || " + mocha.ToString() + " || " + brett.ToString() + " || " + alan.ToString(), new Vector2(0, 400), Color.White);
                    //clickboxes
                    if (!inCams)
                    {
                        _spriteBatch.Draw(debugbox, new Rectangle(0 + officex, 608, 115, 93), new Color(139, 51, 255, 20));
                        _spriteBatch.Draw(debugbox, new Rectangle(2754 + officex, 608, 115, 93), new Color(139, 51, 255, 20));
                        _spriteBatch.Draw(debugbox, new Rectangle(1820 + officex, 515, 15, 15), new Color(139, 51, 255, 20));
                        _spriteBatch.Draw(debugbox, new Rectangle(1415 + officex, 177, 50, 50), new Color(139, 51, 255, 20));
                        _spriteBatch.Draw(debugbox, new Rectangle(0 + officex, 715, 115, 93), new Color(139, 51, 255, 20));
                        _spriteBatch.Draw(debugbox, new Rectangle(2754 + officex, 715, 115, 93), new Color(139, 51, 255, 20));
                        _spriteBatch.Draw(debugbox, new Rectangle(1522 + officex, 568, 128, 36), new Color(139, 51, 255, 20));
                    }
                    if (canCam)
                    {
                        _spriteBatch.Draw(debugbox, new Rectangle(0, 1050, 1920, 30), new Color(3, 227, 252, 20));
                    } else
                    {
                        _spriteBatch.Draw(debugbox, new Rectangle(0, 1050, 1920, 30), new Color(255, 0, 0, 20));
                    }
                }
                else if (Game1.isDebugMode)
                {
                    _spriteBatch.DrawString(pixelfont, "Debug Mode is enabled; Hold Left Ctrl to see instructions", new Vector2(0, 50), Color.White);
                    if (Keyboard.GetState().IsKeyDown(Keys.LeftControl))
                    {
                        _spriteBatch.DrawString(pixelfont, "Press F1 to summon Bonnie to the left door, F2 to summon Chica to the right door, and Left Shift + F2 to summon Freddy to front window", new Vector2(0, 100), Color.White);
                        _spriteBatch.DrawString(pixelfont, "Press F3 to toggle information screen", new Vector2(0, 150), Color.White);
                        _spriteBatch.DrawString(pixelfont, "Press F4 to change Foxy's position; Hold Left Shift to decrease, otherwise it will increase", new Vector2(0, 200), Color.White);
                        _spriteBatch.DrawString(pixelfont, "Press / to half your power", new Vector2(0, 250), Color.White);
                        _spriteBatch.DrawString(pixelfont, "Hold C, D, and press + to end this night", new Vector2(0, 300), Color.White);
                        _spriteBatch.DrawString(pixelfont, "Press F5, F6, F7, F8, or F9 to trigger a jumpscare from Bonnie, Chica, Freddy, Foxy, and Cheesestick respectively", new Vector2(0, 350), Color.White);
                    }
                }
            }
            else
            {
                _spriteBatch.DrawString(defaultfont, "5", new Vector2(960, clockAnimOffset), Color.White);
                _spriteBatch.DrawString(defaultfont, "  AM", new Vector2(960, 540), Color.White);
                _spriteBatch.DrawString(defaultfont, "6", new Vector2(960, clockAnimOffset + 50), Color.White);
                _spriteBatch.Draw(debugbox, new Rectangle(900, 490, 100, 50), Color.Black);
                _spriteBatch.Draw(debugbox, new Rectangle(900, 570, 100, 50), Color.Black);
            }
        }

        public void LoadDraw(SpriteBatch _spriteBatch, GameTime gameTime, SpriteFont defaultfont, SpriteFont pixelfont, Texture2D debugbox)
        {
            if (!Game1.sneakyLoad)
            {
                if (Game1.FreddyLevel == 1 && Game1.BonnieLevel == 9 && Game1.ChicaLevel == 8 && Game1.FoxyLevel == 7)
                {
                    _spriteBatch.DrawString(defaultfont, "nuh uh", new Vector2(950, 540), Color.White);
                }
                else
                if (Game1.saveData.Night != 7)
                {
                    _spriteBatch.DrawString(defaultfont, "Night " + Game1.saveData.Night, new Vector2(950, 540), Color.White);
                }
                else
                    _spriteBatch.DrawString(defaultfont, "Custom Night", new Vector2(900, 540), Color.White);


                _spriteBatch.DrawString(pixelfont, "Loading: " + chunkLoad.ToString("00") + " / 12", new Vector2(935, 570), Color.White);
            }
            if (chunkLoad < 13)
                chunkLoad++;
        }

        public void BonnieJumpscare()
        {
            bonnieAtDoor = false;
            soundchannel[2] = sounds[5].CreateInstance();
            soundchannel[2].Play();
            isJumpScare = true;
            framecounter = 0;
            jumpscareID = 1;
        }
        public void ChicaJumpscare()
        {
            chicaAtDoor = false;
            soundchannel[2] = sounds[5].CreateInstance();
            soundchannel[2].Play();
            isJumpScare = true;
            framecounter = 0;
            jumpscareID = 2;
        }
        public void FreddyJumpscare()
        {
            soundchannel[2] = sounds[5].CreateInstance();
            soundchannel[2].Play();
            isJumpScare = true;
            framecounter = 0;
            jumpscareID = 3;
        }
        public void FoxyJumpscare()
        {
            soundchannel[2] = sounds[5].CreateInstance();
            soundchannel[2].Play();
            isJumpScare = true;
            framecounter = 0;
            jumpscareID = 4;
        }
        public void CheesestickJumpscare()
        {
            cheeseLeftDoor = false;
            cheeseRightDoor = false;
            soundchannel[2] = sounds[5].CreateInstance();
            soundchannel[2].Play();
            isJumpScare = true;
            framecounter = 0;
            jumpscareID = 5;
        }
    }
}
