
using UnityEngine;

/*  TODO: 
 *  Display Victory and Reset the Game
 *  
 */

public class GameManager : MonoBehaviour
{
  #region Data
  private CheckerBoard board;
  [HideInInspector]
  // SUBJECT TO CHANGE, IS THERE ANOTHER WAY TO DO THIS?
  public Movement movement;

  // SUBJECT TO CHANGE
  // The blue player always takes the first turn...
  public bool isBlueTurn = true;
  #endregion

  /// <summary>
  /// Methods: see comments below.
  /// TODO Display Victory and Reset the Game 
  /// </summary>
  #region Helpers
  // Determines Valid Moves i.e. moving horizontally, jumping another piece etc.
  public bool CheckValidMove(Piece[,] board, int x1, int y1, int x2, int y2)
  {
    // Blue Team
    if (isBlueTurn)
    {
      return TeamValidMove(board, x1, y1, x2, y2, 1);
    }

    // Red Team
    if (!isBlueTurn)
    {
      return TeamValidMove(board, x1, y1, x2, y2, -1);
    }
    //Default
    return false;
  }

  public bool TeamValidMove(Piece[,] board, int x1, int y1, int x2, int y2, int val)
  {
    if (board[x2, y2].color != Piece.PieceColor.Empty)
    {
      return false;
    }

    int deltaMoveX = Mathf.Abs(x1 - x2); // Change in movement in x
    int deltaMoveY = y2 - y1; // Change in movement in y

    // If the peice moves once diagonally, it is a valid move
    if (deltaMoveX == Mathf.Abs(val))
    {
      if (deltaMoveY == val)
      {
        return true;
      }

    }

    // If the pieces moves twice diagonally, and jumps a peice, it is a valid move
    else if (deltaMoveX == Mathf.Abs(val * 2))
    {
      if (deltaMoveY == val * 2)
      {
        Piece jumpedPiece = board[(x1 + x2) / 2, (y1 + y2) / 2]; // Average; gives you the middle piece 
        Vector2 jumpedPiecePos = new Vector2((x1 + x2) / 2, (y1 + y2) / 2);
        if (jumpedPiece != null && jumpedPiece.color != movement.selectedPiece.color)
        {
          JumpPiece(board, jumpedPiecePos);
          return true;
        }
      }
    }
    return false;
  }

  // Destroyes the jumped piece and sets it to an empty piece on the board
  public void JumpPiece(Piece[,] board, Vector2 pos)
  {
    Destroy(board[(int)pos.x, (int)pos.y].GO);
    board[(int)pos.x, (int)pos.y] = new Piece();
  }

  public bool CheckWinCondition(Piece[,] board, Piece.PieceColor color)
  {
    // Nested for loop to itterate over every possible peice position
    for (int x = 0; x < 8; x++)
    {
      for (int y = 0; y < 8; y++)
      {
        // Check to see if there are no more pieces of specific color 
        if (board[x, y].color == color)
        {
          // If no more pieces of one team are present return false 
          return false;
        }
      }
    }
    // Otherwise there are still pieces of one team remaining
    return true;
  }

  // TODO Display Victory and Reset the Game 
  public void Victory(Piece.PieceColor color)
  {
    Debug.Log(color);
  }
  #endregion
}
