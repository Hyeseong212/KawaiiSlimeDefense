using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System;
#if UNITY_EDITOR
using UnityEditor;
#endif

namespace _util
{
#if UNITY_EDITOR
    public class AssetSelect
    {
        static public List<T> ObjectSelectsPath<T>(string folder, string ext="*") where T : UnityEngine.Object
        {
            if(folder[folder.Length-1] != '/')
            {
                folder += '/';
            }

            List<string> files = ObjectSelect.ObjectSelectsPath(folder, ext, SearchOption.TopDirectoryOnly);

            List<T> list = new List<T>();

            foreach(string file in files)
            {
                T t = AssetDatabase.LoadAssetAtPath<T>(file);
                if (t == null)
                {
                    continue;
                }
                list.Add(t);
            }
            return list;
        }
        static public List<T> ObjectSelectsPath<T>(string folder, string[] exts) where T : UnityEngine.Object
        {
            if (folder[folder.Length - 1] != '/')
            {
                folder += '/';
            }
            List<string> files = ObjectSelect.ObjectSelectsFileName(folder, exts, SearchOption.TopDirectoryOnly);

            List<T> list = new List<T>();

            foreach (string file in files)
            {
                T t = AssetDatabase.LoadAssetAtPath<T>(file);
                if(t==null)
                {
                    continue;
                }
                list.Add(t);
            }
            return list;
        }
    }
#endif
    public class ObjectSelect
    {

        static public List<string> ObjectSelectsPath(string folder, string type, SearchOption option = SearchOption.TopDirectoryOnly)
        {
            if (folder[folder.Length - 1] != '/')
            {
                folder += '/';
            }
            List<string> list = new List<string>();
            if (!Directory.Exists(folder))
            {
                Directory.CreateDirectory(folder);
            }
            string[] files = Directory.GetFiles(folder, "*." + type, option);
            return new List<string>(files);
        }
        static public List<string> ObjectSelectsFileName(string folder, string[] types, SearchOption option = SearchOption.TopDirectoryOnly)
        {
            if (folder[folder.Length - 1] != '/')
            {
                folder += '/';
            }
            List<string> list = new List<string>();
            if (!Directory.Exists(folder))
            {
                Directory.CreateDirectory(folder);
            }
            List<string> listFile = new List<string>();
            foreach (string type in types)
            {
                string[] r = Directory.GetFiles(folder, "*." + type, option);
                if (r == null || r.Length == 0)
                {
                    continue;
                }
                listFile.AddRange(r);
            }
            string[] files = listFile.ToArray();
            foreach (string s in files)
            {
                string[] r = s.Split(new char[] { '/', '\\' });

                string file = r[r.Length - 1];
                list.Add(file.Substring(0, file.LastIndexOf('.') - 1));
            }
            return list;
        }
#if UNITY_EDITOR
        static public List<UnityEngine.Object> ObjectSelects(string folder, string[] types, SearchOption option = SearchOption.TopDirectoryOnly)
        {
            if (folder[folder.Length - 1] != '/')
            {
                folder += '/';
            }
            List<UnityEngine.Object> list = new List<UnityEngine.Object>();
            if (!Directory.Exists(folder))
            {
                Directory.CreateDirectory(folder);
            }

            List<string> listFile = new List<string>();
            foreach (string type in types)
            {
                string[] r = Directory.GetFiles(folder, "*." + type, option);
                if (r == null || r.Length == 0)
                {
                    continue;
                }
                listFile.AddRange(r);
            }
            string[] files = listFile.ToArray();
            foreach (string s in files)
            {
                string[] r = s.Split(new char[] { '/', '\\' });

                string file = r[r.Length - 1];
                UnityEngine.Object o = AssetDatabase.LoadAssetAtPath<UnityEngine.Object>(folder + file);
                //file = file.Substring(0, file.Length - (1+type.Length) );
                //UnityEngine.Object o = Resources.Load<UnityEngine.Object>(path + Path.DirectorySeparatorChar + file);
                if (o != null)
                {
                    list.Add(o);
                }
            }
            return list;
        }
#endif

    }
}