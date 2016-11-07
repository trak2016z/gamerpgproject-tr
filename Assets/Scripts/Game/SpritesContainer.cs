using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

public class SpritesContainer
{
    protected static SpritesContainer _instance;
    Dictionary<string, Sprite> _spritesContainter;
    private SpritesContainer()
    {
        _spritesContainter = new Dictionary<string, Sprite>();
    }

    public void AddElement(string name, Sprite sprite)
    {
        if (_spritesContainter.ContainsKey(name) || sprite == null) return;

        _spritesContainter.Add(name, sprite);
    }

    public Sprite GetSprite(string name)
    {
        if (_spritesContainter.ContainsKey(name))
        {
            return _spritesContainter[name];
        }
        return null; // zwróć domyśly sprite jesli nie odnaleziono
    }

    public static SpritesContainer GetInstance()
    {
        if (_instance == null)
        {
            _instance = new SpritesContainer();
        }
        return _instance;
    }
}

