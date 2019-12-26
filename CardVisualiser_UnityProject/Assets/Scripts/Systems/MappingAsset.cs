/// Author: Christian Grabolle

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace StratosphereGames.Base
{
    /// <summary>
    /// This class represents a mapping of assets. It assumes that the scritable object created is
    /// called exactly like the class deriving from this base-class.
    /// </summary>
    /// <typeparam name="TAsset">The asset mapping class you want to serialize.</typeparam>
    /// <typeparam name="TEnum">The enum that defines the different types of asset mapped.</typeparam>
    /// <typeparam name="TMappedElement">A single element of your mapping. It has to implement IHasEnumType using TEnum.</typeparam>
    public abstract class MappingAsset<TAsset, TEnum, TMappedElement> : 
        ScriptableObject where TAsset : class where TMappedElement : IHasEnumType<TEnum>
    {
        public List<TMappedElement> Mapping;
        private static TAsset LoadedMapping;

        public TMappedElement GetElementForType(TEnum type)
        {
            var mappedPrefab = Mapping.Find((x) => x.Type.Equals(type));
            return mappedPrefab;
        }

        private static string GetResourceName<T>() where T : MappingAsset<TAsset, TEnum, TMappedElement>, new()
        {
            return typeof(T).Name;
        }

        public static TAsset GetMapping<T>() where T : MappingAsset<TAsset, TEnum, TMappedElement>, new()
        {
            if (LoadedMapping != null)
            {
                return LoadedMapping;
            }
            string resName = GetResourceName<T>();
            var prefabMapping = Resources.Load(resName) as TAsset;
            if (prefabMapping == null)
            {
                Debug.LogFormat("MappingAsset: Missing scriptable object '{0}'!", resName);
                return null;
            }

            LoadedMapping = prefabMapping;

            return prefabMapping;
        }
    }
}
