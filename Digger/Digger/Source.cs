using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System.Threading;

namespace Digger
{
    /// <summary>
    /// Rodzaje przyciskow
    /// </summary>
    public enum Buttons
    {
        btnPlay,
        btnLoadGame,
        btnHelp,
        btnHightScores,
        btnExit,
        btnBack,
        btnEasy,
        btnMedium,
        btnHard,
        btnPlayWin,
        btnExitWin,
        btnMainmenu,
        btnRepeat,
        btnExitPause,
        btnBackPause,
        btnExitLose,
    }

    /// <summary>
    /// Stany rozgryki
    /// </summary>
    public enum GameState
    {
        MainMenu,
        Difficulty,
        Options,
        Help,
        HighScore,
        Playing,
        Pause,
        Load,
        WinGame,
        Login,
        Dead,
        Loss,
    }

    /// <summary>
    /// pozimy trudnosci
    /// </summary>
    enum difficulty
    {
        easy,
        medium,
        hard

    }

    /// <summary>
    /// Klasa przechowuje tekstury i czcionka
    /// </summary>
	class Source
	{
        /// <summary>
        /// tekstura gracza
        /// </summary>
        public static Texture2D tony;
        /// <summary>
        /// tekstura robaka
        /// </summary>
        public static Texture2D robak;
        /// <summary>
        /// tekstura pola
        /// </summary>
        public static Texture2D pole;

        /// <summary>
        /// tekstura tla
        /// </summary>
        public static Texture2D tloScore;
        /// <summary>
        /// tekstura rubinu
        /// </summary>
        public static Texture2D rubin;
        /// <summary>
        /// tekstura diamentu
        /// </summary>
        public static Texture2D diament;
        /// <summary>
        /// tekstura pistoletu
        /// </summary>
        public static Texture2D weapon1;
        /// <summary>
        /// tekstura dzidy
        /// </summary>
        public static Texture2D weapon2;
        /// <summary>
        /// tekstura pocisku pistoletu
        /// </summary>
        public static Texture2D pocisk1;
        /// <summary>
        /// tekstura pocisku dzidy
        /// </summary>
        public static Texture2D pocisk2;
        /// <summary>
        /// tekstura super robaka
        /// </summary>
        public static Texture2D robakS;
        /// <summary>
        /// tekstura pulapki
        /// </summary>
        public static Texture2D glaz;
        /// <summary>
        /// czcionka
        /// </summary>
        public static SpriteFont infBoxFont;
        /// <summary>
        /// Wczytuje tekstury
        /// </summary>
        /// <param name="_tony">tekstura gracza</param>
        /// <param name="_robak">tekstura robaka</param>
        /// <param name="_robakS">tekstura super robaka</param>
        /// <param name="_pole">tekstura pola</param>
        /// <param name="_tloScore">tekstura tla</param>
        /// <param name="_rubin">tekstura rubinu</param>
        /// <param name="_diament">tekstura diamentu</param>
        /// <param name="_infBoxFont">czcionka</param>
        /// <param name="_pistolet">tekstura pistoletu</param>
        /// <param name="_dzida">tekstura dzidy</param>
        /// <param name="_pocisk1">tekstura pocisku pistoletu</param>
        /// <param name="_pocisk2">tekstura pocisku dzidy</param>
        /// <param name="_glaz">tekstura pulapki</param>
        public Source(Texture2D _tony, Texture2D _robak,Texture2D _robakS, Texture2D _pole, Texture2D _tloScore, Texture2D _rubin,
            Texture2D _diament, SpriteFont _infBoxFont, Texture2D _pistolet, Texture2D _dzida, Texture2D _pocisk1, Texture2D _pocisk2, Texture2D _glaz)
        {
            tony = _tony;
            robak = _robak;
            robakS = _robakS;
            pole = _pole;
            tloScore = _tloScore;
            rubin = _rubin;
            diament = _diament;
            infBoxFont = _infBoxFont;
            weapon1 = _pistolet;
            weapon2 = _dzida;
            glaz = _glaz;
            pocisk1 = _pocisk1;
            pocisk2 = _pocisk2;
        }



	}
}
