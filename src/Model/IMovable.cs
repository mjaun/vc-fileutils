namespace VCFileUtils.Model
{
    interface IMovable
    {
        void Move(ContainerWrapper newParent);
        void Remove();
    }
}
