﻿/*
VaNiiMenu

Copyright (c) 2018, gpsnmeajp
All rights reserved.
Redistribution and use in source and binary forms, with or without modification, are permitted provided that the following conditions are met:
1. Redistributions of source code must retain the above copyright notice, this list of conditions and the following disclaimer.
2. Redistributions in binary form must reproduce the above copyright notice, this list of conditions and the following disclaimer in the documentation and/or other materials provided with the distribution.
3. Neither the name of the copyright holder nor the names of its contributors may be used to endorse or promote products derived from this software without specific prior written permission.

THIS SOFTWARE IS PROVIDED BY THE COPYRIGHT HOLDERS AND CONTRIBUTORS "AS IS" AND ANY EXPRESS OR IMPLIED WARRANTIES, INCLUDING, BUT NOT LIMITED TO, THE IMPLIED WARRANTIES OF MERCHANTABILITY AND FITNESS FOR A PARTICULAR PURPOSE ARE DISCLAIMED. IN NO EVENT SHALL THE COPYRIGHT HOLDER OR CONTRIBUTORS BE LIABLE FOR ANY DIRECT, INDIRECT, INCIDENTAL, SPECIAL, EXEMPLARY, OR CONSEQUENTIAL DAMAGES (INCLUDING, BUT NOT LIMITED TO, PROCUREMENT OF SUBSTITUTE GOODS OR SERVICES; LOSS OF USE, DATA, OR PROFITS; OR BUSINESS INTERRUPTION) HOWEVER CAUSED AND ON ANY THEORY OF LIABILITY, WHETHER IN CONTRACT, STRICT LIABILITY, OR TORT (INCLUDING NEGLIGENCE OR OTHERWISE) ARISING IN ANY WAY OUT OF THE USE OF THIS SOFTWARE, EVEN IF ADVISED OF THE POSSIBILITY OF SUCH DAMAGE.
*/
using System;
using System.Text;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using DG.Tweening;
using EasyLazyLibrary;

public class FreeLoupeForWindowWorkerScript : MonoBehaviour
{
    Material material;
    public float scale = 1;

    public Vector2 clipPos = Vector2.zero;
    public bool enable = false;

    void Start()
    {
        material = GetComponent<MeshRenderer>().material;
    }

    //1のとき、max=0
    //2のとき、max=0.5
    //4のとき、max=0.75

    //max = 1f - 1f/scale;

    void Update()
    {
        //処理がオン出ない場合は操作しない
        if (enable)
        {
            //Nan, アンダー対策
            if (scale < 1 || float.IsNaN(scale))
            {
                scale = 1;
            }

            //最小位置にクリップ(超えると折り返す)
            if (clipPos.x < 0)
            {
                clipPos.x = 0;
            }
            if (clipPos.y < 0)
            {
                clipPos.y = 0;
            }

            //最大位置にクリップ(超えると折り返す)
            float max = 1f - (1f / scale); //最大位置はスケールに依存する
            if (clipPos.x > max)
            {
                clipPos.x = max;
            }
            if (clipPos.y > max)
            {
                clipPos.y = max;
            }


            //位置を反映
            material.SetTextureOffset("_MainTex", clipPos);
            //スケールを反映(均等に拡大する)
            material.SetTextureScale("_MainTex", Vector2.one / scale);
        }
        else {
            //初期値を設定
            material.SetTextureOffset("_MainTex", Vector2.zero);
            material.SetTextureScale("_MainTex", Vector2.one);
        }
    }
}
