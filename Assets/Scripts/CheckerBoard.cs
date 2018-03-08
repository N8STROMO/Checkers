using UnityEngine;

/*  TODO:
 *  Piece - isKing
 *  
 */

/// <summary>
/// Holds information about a piece i.e. color GameObject etc.
/// TODO: isKing.
/// </summary>
public class Piece
{
  // List the types of peieces.
  public enum PieceColor
  {
    Blue, Red, Empty
  }

  // Default value for PieceColor is Empty.
  public PieceColor color = PieceColor.Empty;

  // Reference to the GameObject.
  public GameObject GO;

  // TODO SUBJECT TO CHANGE.
  public bool isKing = false;
}

public class CheckerBoard : MonoBehaviour
{

  #region Data
  // Checkerboard is based off an 8x8 grid.
  public Piece[,] pieces = new Piece[8, 8];
  private Movement movement;
  private GameManager manager;
  public GameObject redPiece;
  public GameObject bluePiece;
  #endregion

  /// <summary>
  /// Generates board and places the pieces appropriately for checkers. 
  /// </summary>
  #region Events
  // On Initilization generate board.
  private void Start()
  {
    GenerateBoard();
  }

  private void GenerateBoard()
  {
    // Nested for loop to itterate over every possible peice position.
    for (int x = 0; x < 8; x++)
    {
      for (int y = 0; y < 8; y++)
      {
        // For every piece location generate a new, "empty" piece.
        // This information becomes useful to determine a win condition.
        pieces[x, y] = new Piece();
      }
    }

    // Nested for loop to itterate over every possible peice position.
    for (int y = 0; y < 8; y++)
    {
      for (int x = 0; x < 8; x++)
      {
        // Bounds for the blue and red pieces. 
        // The blue pieces spawn in the first three rows, the red pieces spawn in the last three rows, bottom to top.
        if (y < 3 || y > 4)
        {
          // Generate every other peice within the rows.
          if (x % 2 != 0)
          {
            // If it is an even row...
            if (y % 2 == 0)
            {
              GeneratePiece(x, y);
            }
            // If it is an odd row, offset the row by -1...
            else
            {
              GeneratePiece(x - 1, y);
            }
          }
        }
      }
    }
  }
  #endregion


  /// <summary>
  /// Methods: generates piece given a position on the board.
  /// </summary>
  #region Helpers
  private void GeneratePiece(int x, int y)
  {
    // Ternary operator, if the piece falls within the first three rows, it is a blue piece, else it is a red peice. 
    bool isBluePiece = (y > 3) ? false : true;
    // Generate GameObjects. Instantiate a bluePiece if isBluePiece is true otherwise redPiece.
    GameObject generate = Instantiate((isBluePiece) ? bluePiece : redPiece) as GameObject;
    // Generate new instance of Piece.
    Piece piece = new Piece();
    // Set Piece's gameobject to generate. 
    piece.GO = generate;
    // Set the piece to it's type of color.
    if (isBluePiece)
      piece.color = Piece.PieceColor.Blue;
    else
      piece.color = Piece.PieceColor.Red;
    // Set the position of piece.
    pieces[x, y] = piece;
    // Move the piece to its position.
    Movement.MovePiece(piece, x, y);
  }
  #endregion
}