using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "BoardData", menuName = "Chess/New Board Data")]
public class BoardData : ScriptableObject
{
  public Space[] board;
}

public class Space
{
  public GameObject piece;

  public Space(GameObject _piece)
  {
    this.piece = _piece;
  }
}