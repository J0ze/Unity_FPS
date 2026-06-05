using UnityEngine;
namespace Script.InputReader{
    public interface IInputReader
    {
        public void FetchInput(ref InputData.RawInputData rawInputData);
    }
}