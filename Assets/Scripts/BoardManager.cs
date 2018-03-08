using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoardManager : MonoBehaviour {

  public BoardData boardData;

  public GameObject redPiece, bluePiece;

  private void Awake()
  {
    IntializeBoard();
    GenerateBoard();
  }

  private void IntializeBoard()
  {
    boardData.board = new Space[8 * 8];

    ClearBoard();
  }

  private void ClearBoard()
  {
    for(int x = 0; x < 8; x++)
    {
      for (int y = 0; y < 8; y++)
      {
        boardData.board[(8 * y) + x] = new Space(null);
      }
    }
  }

  private void GenerateBoard()
  {
    for (int x = 0; x < 8; x++)
    {
      if(x % 2 == 0)
      {
      GeneratePiece(x, 0, redPiece);
      }
    }

    for (int x = 0; x < 8; x++)
    {
      if (x % 2 == 1)
      {
        GeneratePiece(x, 2, redPiece);
      }
    }
  }
    
  private void GeneratePiece(int x, int y, GameObject piecePrefab)
  {
    if (!piecePrefab.GetComponent<PieceBehavior>()) return;

    GameObject pieceObject = Instantiate(piecePrefab);
    pieceObject.transform.position = new Vector3(x, 0, y);
    boardData.board[(8 * y) + x].piece = pieceObject;
  }
}
