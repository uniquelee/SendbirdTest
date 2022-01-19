using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public static class TransformUtil
{
    public static void Reset(this Transform target, bool isLocal = true)
    {
        if (isLocal)
        {
            target.localPosition = Vector3.zero;
            target.localScale = Vector3.one;
            target.localRotation = Quaternion.identity;
        }
        else
        {
            target.position = Vector3.zero;
            target.localScale = Vector3.one;
            target.rotation = Quaternion.Euler(Vector3.zero);
        }
    }

    public static void ResetPosition(this Transform target, bool isLocal = true)
    {
        if (isLocal)
        {
            target.localPosition = Vector3.zero;
        }
        else
        {
            target.position = Vector3.zero;
        }
    }

    public static void ResetScale(this Transform target, bool isLocal = true)
    {
        if (isLocal)
        {
            target.localScale = Vector3.one;
        }
        else
        {
            target.localScale = Vector3.one;
        }
    }

    public static void ResetRotation(this Transform target, bool isLocal = true)
    {
        if (isLocal)
        {
            target.localRotation = Quaternion.identity;
        }
        else
        {
            target.rotation = Quaternion.Euler(Vector3.zero);
        }
    }

    public static void Copy(this Transform target, Transform goal)
    {
        target.SetParent(goal.parent);
        target.localScale = goal.localScale;
        target.localRotation = goal.localRotation;
        target.SetPosition(goal.localPosition);
    }

    // 포지션션값 적용하기
    public static void SetPosition(this Transform target, Vector3 position, bool isLocal = true)
    {
        if (isLocal)
        {
            target.localPosition = position;
        }
        else
        {
            target.position = position;
        }
    }


    // 포지션션값 X 적용하기    
    public static void SetPositionX(this Transform target, float position, bool isLocal = true)
    {
        if (isLocal)
        {
            Vector3 pos = target.localPosition;
            pos.x = position;
            target.localPosition = pos;
        }
        else
        {
            Vector3 pos = target.position;
            pos.x = position;
            target.position = pos;
        }
    }

    // 포지션션값 Y 적용하기
    public static void SetPositionY(this Transform target, float position, bool isLocal = true)
    {
        if (isLocal)
        {
            Vector3 pos = target.localPosition;
            pos.y = position;
            target.localPosition = pos;
        }
        else
        {
            Vector3 pos = target.position;
            pos.y = position;
            target.position = pos;
        }
    }

    // 포지션션값 Z 적용하기    
    public static void SetPositionZ(this Transform target, float position, bool isLocal = true)
    {
        if (isLocal)
        {
            Vector3 pos = target.localPosition;
            pos.z = position;
            target.localPosition = pos;
        }
        else
        {
            Vector3 pos = target.position;
            pos.z = position;
            target.position = pos;
        }
    }
    
    // 로테이션값 적용하기    
    public static void SetRotation(this Transform target, Vector3 rotation, bool isLocal = true)
    {
        if (isLocal)
        {
            target.localRotation = Quaternion.Euler(rotation);
        }
        else
        {
            target.rotation = Quaternion.Euler(rotation);
        }
    }

    // 로테이션값 X 적용하기    
    public static void SetRotation(this Transform target, Quaternion rotation, bool isLocal = true)
    {
        if (isLocal)
        {
            target.localRotation = rotation;
        }
        else
        {
            target.rotation = rotation;
        }
    }

    // 로테이션값 X 적용하기    
    public static void SetRotationX(this Transform target, float rotation, bool isLocal = true)
    {
        if (isLocal)
        {
            Vector3 orizinAngle = target.localRotation.eulerAngles;
            orizinAngle.x = rotation;
            target.localRotation = Quaternion.Euler(orizinAngle);
        }
        else
        {
            Vector3 orizinAngle = target.rotation.eulerAngles;
            orizinAngle.x = rotation;
            target.rotation = Quaternion.Euler(orizinAngle);
        }
    }

    // 로테이션값 Y 적용하기 
    public static void SetRotationY(this Transform target, float rotation, bool isLocal = true)
    {
        if (isLocal)
        {
            Vector3 orizinAngle = target.localRotation.eulerAngles;
            orizinAngle.y = rotation;
            target.localRotation = Quaternion.Euler(orizinAngle);
        }
        else
        {
            Vector3 orizinAngle = target.rotation.eulerAngles;
            orizinAngle.y = rotation;
            target.rotation = Quaternion.Euler(orizinAngle);
        }
    }

    // 로테이션값 Y 적용하기
    public static void SetRotationZ(this Transform target, float rotation, bool isLocal = true)
    {
        if (isLocal)
        {
            Vector3 orizinAngle = target.localRotation.eulerAngles;
            orizinAngle.z = rotation;
            target.localRotation = Quaternion.Euler(orizinAngle);
        }
        else
        {
            Vector3 orizinAngle = target.rotation.eulerAngles;
            orizinAngle.z = rotation;
            target.rotation = Quaternion.Euler(orizinAngle);
        }
    }

    // 포지션값 더하기   
    public static void AddPosition(this Transform target, Vector3 pos, bool isLocal = true)
    {
        if (isLocal)
        {
            Vector3 orizinPos = target.localPosition;
            orizinPos += pos;
            target.localPosition = orizinPos;
        }
        else
        {
            Vector3 orizinPos = target.position;
            orizinPos += pos;
            target.position = orizinPos;
        }
    }

    // 포지션값 더하기 
    public static void AddPositionY(this Transform target, float pos, bool isLocal = true)
    {
        if (isLocal)
        {
            Vector3 orizinPos = target.localPosition;
            orizinPos.y += pos;
            target.localPosition = orizinPos;
        }
        else
        {
            Vector3 orizinPos = target.position;
            orizinPos.y += pos;
            target.position = orizinPos;
        }
    }
   
    // 로테이션값 더하기   
    public static void AddRotation(this Transform target, Vector3 rotation, bool isLocal = true)
    {
        if (isLocal)
        {
            Vector3 orizinAngle = target.localRotation.eulerAngles;
            orizinAngle += rotation;
            target.localRotation = Quaternion.Euler(orizinAngle);
        }
        else
        {
            Vector3 orizinAngle = target.rotation.eulerAngles;
            orizinAngle += rotation;
            target.rotation = Quaternion.Euler(orizinAngle);
        }
    }

    // 로테이션값 Y 더하기
    public static void AddRotationY(this Transform target, float rotation, bool isLocal = true)
    {
        if (isLocal)
        {
            Vector3 orizinAngle = target.localRotation.eulerAngles;
            orizinAngle.y += rotation;
            target.localRotation = Quaternion.Euler(orizinAngle);
        }
        else
        {
            Vector3 orizinAngle = target.rotation.eulerAngles;
            orizinAngle.y += rotation;
            target.rotation = Quaternion.Euler(orizinAngle);
        }
    }

    // 사이즈값 Y 적용하기
    public static void SetSizeY(this RectTransform target, float sizeY)
    {
        Vector2 size = target.sizeDelta;
        size.y = sizeY;
        target.sizeDelta = size;
    }

    // 객체의 자식들까지 레이어 변경
    static public void SetLayerRecursively(this Transform target, int layer)
    {
        target.gameObject.layer = layer;
        for (int i = 0; i < target.childCount; i++)
        {
            Transform child = target.GetChild(i);
            SetLayerRecursively(child, layer);
        }
    }
}
