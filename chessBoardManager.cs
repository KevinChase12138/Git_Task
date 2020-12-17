using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class chessBoardManager : MonoBehaviour
{

    public enum Direction{up, down, left, right};
    public Direction direction;
    
    public Rigidbody2D rb;
    public SpriteRenderer sr;



    [Header("information")]
    public GameObject roomPerfab;
    public int roomNumberX;
    public int roomNumberY;
    public Color mainColor, SecondaryColor;

    
    [Header("positioning")]
    public Transform generatorPoint;
    public float xOffset, yOffset;


    [Header("timing")]
    public int i=0;
    public int j=0;
    public int inDex=0;


    public double timeRest;
    public double lastTime;
    public double curTime;

    public float passedSeconds;

    [Header("speed")]

    public float speed;
    public float generatingSpeed;

    // [Header("pointing")]
    // public bool isOver = false;
    // public Animator FLoor;


    public List<GameObject> rooms = new List<GameObject>();


    // Start is called before the first frame update
    void Start()
    {

    }


    // Update is called once per frame
    void Update() {
        timePass(100);
        curTime2 = Time.time;

        curTime = Time.time;
        if(curTime - lastTime >= generatingSpeed)
        {
            //计时器部分
            timeRest = timeRest - generatingSpeed;
            lastTime = curTime;

            if(i < roomNumberX){
                if(j < roomNumberY){
                    rooms.Add(Instantiate(roomPerfab, generatorPoint.position, Quaternion.identity));
                    
                    rb = rooms[inDex+j+i].GetComponent<Rigidbody2D>();

                    // 显示层级
                    rooms[inDex+j+i].transform.position = new Vector3
                        (
                            rooms[inDex+j+i].transform.position.x,
                            rooms[inDex+j+i].transform.position.y,
                            (roomNumberX*roomNumberY - rooms[inDex+j+i].transform.position.z-inDex-j-i)
                        );

                    // 加速运动，减速由gameobject的rigidbody2d的linearlag完成
                    rb.velocity = new Vector2(0,speed);

                    //改变point位置
                    ChangePointPos(0);


                    // Debug.Log(rooms[inDex+j+i].transform.GetSiblingIndex());

                    // Debug.Log(inDex+i+j);
                    // Debug.Log(16-inDex-i-j);

                    j++;
                }if(j == roomNumberY){

                    ChangePointPos(1);
                    i=i+1;
                    for(int k=0; k<roomNumberY;k++){
                        ChangePointPos(2);
                    }
                    j=0;
                    inDex = inDex + roomNumberY-1;
                }
            }
        }
    }


    public void ChangePointPos(int direct)
    {
        direction = (Direction) direct;
        // Random.range报错，需用UnityEngine。Random。Range（R大写）

        switch(direction)
        {
            case Direction.up:
                generatorPoint.position += new Vector3(-xOffset, -yOffset, 0);
                break;
            case Direction.down:
                generatorPoint.position += new Vector3(xOffset, -yOffset, 0);
                break;
            case Direction.left:
                generatorPoint.position += new Vector3(xOffset, yOffset, 0);
                break;
        }
    }

    
    public float curTime2,lastTime2,timeRest2=100;
    // 原计时器
    void timePass(double passedSeconds)
    {   

        if (curTime2 - lastTime2 >= 1f)
        {   
            timeRest2 = timeRest2 - 1f;
            lastTime2 = curTime2;
            Debug.Log(timeRest2);
        }


    }

    // public void OnMouseEnter() {
    //     anim.SetBool("isOver", true);
    // }

    // public void OnMouseExit() {
    //     anim.SetBool("isOver", false);
    // }

}
