﻿using System;

namespace ConsoleChess.tabuleiro {
    public class TabuleiroException : Exception {
        
        public TabuleiroException(string message) : base(message) {
        }
    }
}