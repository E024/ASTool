﻿using System;
using System.Collections.Generic;
using System.Text;

namespace ASBinCode.rtData
{
    public sealed class rtArray : IRunTimeValue
    {
        public class arrayObjHandle
        {
            public rtObject bindArrayObject;
            public override int GetHashCode()
            {
                if (bindArrayObject == null)
                {
                    return 0.GetHashCode();
                }
                else
                {
                    return bindArrayObject.GetHashCode();
                }
            }

            public override bool Equals(object obj)
            {
                arrayObjHandle right = obj as arrayObjHandle;
                if (right == null)
                {
                    return false;
                }
                return ReferenceEquals(bindArrayObject, right.bindArrayObject);
            }

        }

        internal List<IRunTimeValue> array;
        public List<IRunTimeValue> innerArray {
            get { return array; } }

        public arrayObjHandle objHandle;
        
        public rtArray()
        {
            array = new List<IRunTimeValue>();
            objHandle = new arrayObjHandle();
            objHandle.bindArrayObject = null;
        }
        

        

        public RunTimeDataType rtType
        {
            get
            {
                return RunTimeDataType.rt_array;
            }
        }

        public override string ToString()
        {
            StringBuilder asb = new StringBuilder();
            for (int i = 0; i < array.Count && i<256; i++)
            {
                asb.Append( array[i].rtType !=RunTimeDataType.rt_void ?( array[i].rtType==RunTimeDataType.rt_string? ((rtString)array[i]).valueString() :  array[i].ToString()):String.Empty);
                asb.Append(",");
            }

            if (asb.Length > 0)
            {
                asb.Remove(asb.Length - 1, 1);
            }

            return asb.ToString();
            //return base.ToString();
        }


        public object Clone()
        {

            rtArray result = new rtArray();
            result.CopyFrom(this);
            return result;
        }

        public void CopyFrom(rtArray right)
        {
            //_objid = right._objid;
            array = right.array;
            objHandle = right.objHandle;
        }

        public override int GetHashCode()
        {
            return array.GetHashCode() ^ objHandle.GetHashCode();
        }

        public override bool Equals(object obj)
        {
            rtArray right = obj as rtArray;
            if (right == null)
            {
                return false;
            }

            return array.Equals(right.array) && objHandle.Equals(right.objHandle);
        }


    }
}