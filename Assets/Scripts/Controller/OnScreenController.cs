using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class OnScreenController : MonoBehaviour
{
    public static Vector2 _2DimentionRatio;

    [SerializeField] RectTransform ScreenAnalog;
    [SerializeField] RectTransform minX,minY,maxX, maxY;


    public Action<float> OnHorizontalMove;
    public Action<float> OnVerticalMove;
    public Action OnSwipLeft;
    public Action OnSwipRight;
    public Action OnSwipUp;
    public Action OnSwipDown;
    public Action OnHold;
    public Action OnRelessed;
    public Action OnTap;
    public Action OnDoubleTap;
    public Action OnTapRepeat;


    Vector2 startpos = Vector2.zero, endpos = Vector2.zero;

    bool isSwipping;
    bool isInputAction;
    bool isHolding;

    RectTransform analogParent;
    CanvasGroup analogParentCanvas;
    // Start is called before the first frame update
    void Start()
    {
        analogParent = GetComponent<RectTransform>();
        analogParentCanvas = GetComponent<CanvasGroup>();
    }

    // Update is called once per frame
    void Update()
    {
        Swipping();
        _2DimentionRatio =  Moving();


    }

    public Vector2 Moving()
    {
        float YPosition = 0, XPosition = 0 ;

        Vector2 pos = Vector2.zero;


        if (Input.GetMouseButton(0))
        {
   
            ScreenAnalog.position =Input.mousePosition;
            
            if(isHolding)
            {
                if(OnHold != null)
                {
                    OnHold();
                }

            }

            if (ScreenAnalog.position.y > maxY.position.y)
            {
                YPosition = maxY.position.y;
            }
            else if (ScreenAnalog.position.y < minY.position.y)
            {
                YPosition = minY.position.y;
            }
            if (ScreenAnalog.position.x > maxX.position.x)
            {
                XPosition = maxX.position.x;
            }
            else if (ScreenAnalog.position.x < minX.position.x)
            {
                XPosition = minX.position.x;
            }
            ScreenAnalog.position = new Vector2(XPosition > 0 ? XPosition : ScreenAnalog.position.x,YPosition>0? YPosition:ScreenAnalog.position.y);

            pos.x =  ScreenAnalog.anchoredPosition.x/100;

            pos.y = ScreenAnalog.anchoredPosition.y / 100;
         

            if(OnVerticalMove != null)
            {
                OnVerticalMove(pos.y);
            }
            if(OnHorizontalMove != null)
            {
                OnHorizontalMove(pos.x);
            }


        }
        else
        {
            ScreenAnalog.anchoredPosition = Vector2.zero;
        }

        return pos;
    }

    public void Swipping()
    {
        CheckTap();
        if(Input.GetMouseButtonDown(0))
        {
            SetPositionToMouse();
            startpos = Input.mousePosition;
            isSwipping = true;
            StartCoroutine(Delay(0.2f, () => { isSwipping = false; }));
            StartCoroutine(Delay(0.2f,()=> { isHolding = true; }));
            OnShowAnalog();
        }
        if (Input.GetMouseButtonUp(0))
        {
            if (isSwipping)
            {
                endpos = Input.mousePosition;
                Vector2 temp = startpos - endpos;

                if (temp.x > 50)
                {
                    if(OnSwipLeft != null)
                    OnSwipLeft();
                    print("Left");
                }
                else if (temp.x < -50)
                {
                    if (OnSwipRight != null)
                        OnSwipRight();
                    print("right");
                }
                else if (temp.y > 50)
                {
                    if (OnSwipDown != null)
                        OnSwipDown();
                    print("Down");
                }
                else if (temp.y < -50)
                {
                    if (OnSwipUp != null)
                        OnSwipUp();
                    print("Up");
                }
                //for animate Swip animation or particle
                OnHideAnalog();
            }
            else
            {
                
                OnHideAnalog();
            }
            Tapping();
        }
    }
    float tapCooldown;
    int tapCount;
    public void Tapping()
    {
        tapCooldown = 0.18f;

        if (tapCount > 2)
        {
            if (OnTapRepeat != null)
            {
                OnTapRepeat();

            }
            print("Tap Repeat");
        }
        else
            tapCount++;

    }
    public void CheckTap()
    {
     
        
        if (tapCooldown > 0)
            tapCooldown -= Time.deltaTime;
        else
        {
            if (tapCount > 1)
            {
                if (OnDoubleTap != null)
                {
                    OnDoubleTap();

                }
                print("On Double Tap");
            }
            else if (tapCount >0)
            {
                if (OnTap != null)
                {
                    OnTap();
                   
                }
                print("Tap");
            }
            tapCount = 0;
        }
      
    }
    public IEnumerator Delay(float time,Action callback)
    {
        yield return new WaitForSeconds(time);
        callback();
    }
    public void SetPositionToMouse()
    {
        transform.position = Input.mousePosition;
    }
    public void OnShowAnalog()
    {
        LeanTween.alphaCanvas(analogParentCanvas, 0.5f, 0.1f);
        LeanTween.scale(analogParent, Vector3.one, 0.1f).setEaseOutBounce();
    }
    public void OnHideAnalog()
    {
        LeanTween.alphaCanvas(analogParentCanvas, 0, 0.2f);
        LeanTween.scale(analogParent, Vector3.one * 0.8f, 0.2f).setEaseInBounce();
    }
}
