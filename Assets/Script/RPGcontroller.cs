using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.Tilemaps;
using UnityEngine.UIElements;

public class RPGController : MonoBehaviour
{
    public float speed;
    private Vector2 movement;
    public Tilemap map;

    public Animator animator;


    public Transform targetPosition;

    public LayerMask UnwalkableLayer;
    public LayerMask MoveableLayer;
    [Header("Level 1")]
    public List<GameObject> allBlocksLvl1;
    public GameObject rocklvl1;

    [Header("Level 2")]
    public List<GameObject> allBlocksLvl2;
    public GameObject rocklvl2;

        [Header("Level 3")]
    public List<GameObject> allBlocksLvl3;

    [Header("reset")]
    public int lvlTracker = 1;

    // Update is called once per frame
    private void Awake(){
        targetPosition.position = transform.position;
        animator = gameObject.GetComponent<Animator>();
    }
    void Update()
    {

                // level reset
        if (Input.GetKeyDown("r"))
            {
                if(lvlTracker == 1) {
                    transform.position = new Vector3(-1.5f, -0.5f, 0f);
                    targetPosition.transform.position = new Vector3(-1.5f, -0.5f, 0f);
                    allBlocksLvl1[0].transform.position = new Vector3(0.5f, -1.5f, 0f);
                    allBlocksLvl1[1].transform.position = new Vector3(2.5f, -0.5f, 0f);
                    allBlocksLvl1[0].GetComponent<BoxController>().boxTarget = new Vector3(0.5f, -1.5f, 0f);
                    allBlocksLvl1[1].GetComponent<BoxController>().boxTarget = new Vector3(2.5f, -0.5f, 0f);
                }
                if(lvlTracker == 2) {
                    transform.position = new Vector3(1.5f, 4.5f, 0f);
                    targetPosition.transform.position = new Vector3(1.5f, 4.5f, 0f);
                    allBlocksLvl2[0].transform.position = new Vector3(2.5f, 6.5f, 0f);
                    allBlocksLvl2[1].transform.position = new Vector3(-6.5f, 4.5f, 0f);
                    allBlocksLvl2[0].GetComponent<BoxController>().boxTarget = new Vector3(2.5f, 6.5f, 0f);
                    allBlocksLvl2[1].GetComponent<BoxController>().boxTarget = new Vector3(-6.5f, 4.5f, 0f);
                }
                if(lvlTracker == 3) {
                    transform.position = new Vector3(5.5f, 12.5f, 0f);
                    targetPosition.transform.position = new Vector3(5.5f, 12.5f, 0f);
                    allBlocksLvl3[0].transform.position = new Vector3(8.5f, 9.5f, 0f);
                    allBlocksLvl3[1].transform.position = new Vector3(12.5f, 9.5f, 0f);
                    allBlocksLvl3[2].transform.position = new Vector3(9.5f, 13.5f, 0f);
                    allBlocksLvl3[0].GetComponent<BoxController>().boxTarget = new Vector3(8.5f, 9.5f, 0f);
                    allBlocksLvl3[1].GetComponent<BoxController>().boxTarget = new Vector3(12.5f, 9.5f, 0f);
                    allBlocksLvl3[2].GetComponent<BoxController>().boxTarget = new Vector3(9.5f, 13.5f, 0f);
                }
            }
        //check lvl1 completion
        bool everythingInPlacelvl1 = false;
        foreach(GameObject g in allBlocksLvl1) {
            if(!g.GetComponent<Triggers>().inPlace) {
                everythingInPlacelvl1 = false;
                break;
            }
            else everythingInPlacelvl1 = true;
        }
        if(everythingInPlacelvl1) {
            Destroy(rocklvl1);
            lvlTracker = 2;
        }
        //check lvl2 completion
        bool everythingInPlacelvl2 = false;
        foreach(GameObject g in allBlocksLvl2) {
            if(!g.GetComponent<Triggers>().inPlace) {
                everythingInPlacelvl2 = false;
                break;
            } else everythingInPlacelvl2 = true;
        }
        if(everythingInPlacelvl2) {
            Destroy(rocklvl2);
            lvlTracker = 2;
        }

        bool everythingInPlacelvl3 = false;
        foreach(GameObject g in allBlocksLvl3) {
            if(!g.GetComponent<Triggers>().inPlace) {
                everythingInPlacelvl3 = false;
                break;
            } else everythingInPlacelvl3 = true;
        }
        if(everythingInPlacelvl3) {
            SceneManager.LoadScene (sceneName:"EndScene");
        }
        //Animation
        //if(movement.x != 0 || movement.y != 0) animator.SetFloat("Speed", 0.1f);
        //else animator.SetFloat("Speed", 0f);
        if(Input.GetKey(KeyCode.UpArrow) ||Input.GetKey(KeyCode.DownArrow)||Input.GetKey(KeyCode.LeftArrow)||Input.GetKey(KeyCode.RightArrow)) animator.SetFloat("Speed", 0.1f);
        else animator.SetFloat("Speed", 0f);

        //collision checkers
        
        if (Vector3.Distance(transform.position, targetPosition.position) < .01f && 
            !Physics2D.OverlapCircle(targetPosition.position + new Vector3(movement.x, movement.y, 0f), .1f, UnwalkableLayer)) //if moving and not directly facing wall then
            {
                Collider2D g = Physics2D.OverlapCircle(targetPosition.position + new Vector3(movement.x, movement.y, 0f), .1f, MoveableLayer); //if player is pushing a block, that block is now g
                if(g) { //g means you are actively have your targetPosition on a block
                    if(!Physics2D.OverlapCircle(targetPosition.position + new Vector3(2 * movement.x, 2 * movement.y, 0f), .1f, UnwalkableLayer) &&
                    !Physics2D.OverlapCircle(targetPosition.position + new Vector3(2*movement.x, 2*movement.y, 0f), .1f, MoveableLayer)) {//and block on block
                        //if block not against a wall then both player and block can move

                        targetPosition.position = new Vector3(targetPosition.position.x + movement.x, targetPosition.position.y + movement.y, 0f);
                        g.gameObject.GetComponent<BoxController>().boxTarget = new Vector3(targetPosition.position.x + movement.x, targetPosition.position.y + movement.y, 0f);
                        //g.gameObject.transform.position = Vector3.MoveTowards(g.gameObject.transform.position, new Vector3(targetPosition.position.x + movement.x, targetPosition.position.y + movement.y, 0f), speed * Time.deltaTime * 1.1f);
                    }
                } else {
                    targetPosition.position = new Vector3(targetPosition.position.x + movement.x, targetPosition.position.y + movement.y, 0f);
                }
                
        }
        if(movement.x != 0 & movement.y != 0) {}
        else transform.position = Vector3.MoveTowards(transform.position, targetPosition.position, speed * Time.deltaTime);
    }

    void OnMove(InputValue value)
    {
        movement = value.Get<Vector2>();
        //check both x & y not 0 then make move 0
    }

    private void OnCollisionEnter2D(Collision2D other) {
        if(other.gameObject.CompareTag("Cat")) {
            //other.gameObject.transform.position = new Vector3(targetPosition.position.x + movement.x, targetPosition.position.y + movement.y, 0f);
        }
    }
}
