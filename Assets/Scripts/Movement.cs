using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*  TODO:
 *  Bounds Checking SelectPiece()
 */

public class Movement : MonoBehaviour
{

  #region Data
  // Create references to other classes: CheckerBoard, GameManager, Piece
  private CheckerBoard board;
  private GameManager manager;
  public Piece selectedPiece;

  // Stores information about the location of the mouse
  private Vector2 mouseOver;
  // Stores information about where the piece is being dragged from
  private Vector2 dragStart;
  // Stores information about where the piece is being dragged to
  private Vector2 dragEnd;
  #endregion

  /// <summary>
  /// Mouse control. Following the rules. Everything to deal with gameplay movement.
  /// </summary>
  #region Events
  private void Start()
  {
    board = GetComponent<CheckerBoard>();
    manager = GetComponent<GameManager>();
    manager.movement = this; // Could this be manager = GetComponent<Movement>(); ?
  }

  // TODO Some of this can be extracted out elsewhere?
  private void Update()
  {

    UpdateMouseOver();
    Debug.Log(mouseOver);

    // If there is a piece selected.
    if (selectedPiece != null)
    {
      // If the left mouse button is clicked.
      if (Input.GetMouseButton(0))
      {
        // Transform the position of the piece relative to where the mouse is over.
        UpdatePieceDrag(selectedPiece);
      }
    }

    int x = (int)mouseOver.x;
    int y = (int)mouseOver.y;

    // If the mouse button is being held down.
    if (Input.GetMouseButtonDown(0))
    {
      // Select the piece where the mouse is currently located.
      SelectPiece(x, y);
      // 
      board.pieces[(int)mouseOver.x, (int)mouseOver.y] = new Piece();
    }

    // If the mouse button is released.
    if (Input.GetMouseButtonUp(0))
    {
      // Set dragEnd to the position of the mouse.
      dragEnd = new Vector2((int)mouseOver.x, (int)mouseOver.y);

      // If it is a valid move...
      if (manager.CheckValidMove(board.pieces, (int)dragStart.x, (int)dragStart.y, (int)dragEnd.x, (int)dragEnd.y))
      {
        // Set the selected piece to where the current mouse position.
        // NOTE: VERY LUCKY HOW BOARD ALIGNS WITH UNITY GRID. THIS WOULD NORMALLY NOT WORK!!!
        board.pieces[(int)mouseOver.x, (int)mouseOver.y] = selectedPiece;
        // Update the movement of the piece.
        UpdateMouseOver();
        // Move the selected piece.
        selectedPiece.GO.transform.position = new Vector2((int)mouseOver.x, (int)mouseOver.y);
        // The piece has been moved and is no longer the selected peice.
        selectedPiece = null;

        //Swap turns. 
        manager.isBlueTurn = !manager.isBlueTurn;

      }
      else // Snap the piece back to it's initial location.
      {
        // Set the selected peiece to where the mouse started dragging the piece. 
        board.pieces[(int)dragStart.x, (int)dragStart.y] = selectedPiece;
        // Move the piece back to where it intially was; it was not a valid move, snap back.
        MovePiece(selectedPiece, (int)dragStart.x, (int)dragStart.y);
        // The piece tried to move, however incorrectly, and is not longer the selected piece. 
        selectedPiece = null;
      }

      if (manager.CheckWinCondition(board.pieces, Piece.PieceColor.Blue))
      {
        manager.Victory(Piece.PieceColor.Red);
      }
      else if (manager.CheckWinCondition(board.pieces, Piece.PieceColor.Red))
      {
        manager.Victory(Piece.PieceColor.Blue);
      }

    }
  }
  #endregion

  /// <summary>
  ///  Methods: Finds the mouse position. Selects a piece. Where is the mouse going? Moves the piece.
  /// </summary>
  #region Helpers 
  public void UpdateMouseOver()
  {
    Vector2 mousePosition = (Vector2)Camera.main.ScreenToWorldPoint(Input.mousePosition) + (Vector2.one / 2);
    mouseOver = new Vector2(mousePosition.x, mousePosition.y);
  }

  public void SelectPiece(int x, int y)
  {
    // Bounds checking? 
    Piece p = board.pieces[x, y];
    if (p != null && p.color != Piece.PieceColor.Empty)
    { 
      selectedPiece = p;
      dragStart = new Vector2(x, y);
    }
  }

  // TODO clean up Update() here.
  public void TryMove()
  {

  }

  public void UpdatePieceDrag(Piece p)
  {
    UpdateMouseOver();
    p.GO.transform.position = mouseOver - (Vector2.one / 2);
  }

  public static void MovePiece(Piece p, int x, int y)
  {
    p.GO.transform.position = (Vector3.right * x) + (Vector3.up * y);
  }
  #endregion
}
