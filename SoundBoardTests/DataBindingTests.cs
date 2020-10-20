using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using FluentAssertions;
using SoundBoardDotNet.DataBinding;

namespace SoundBoardTests
{
    [TestClass]
    public class DataBindingTests
    {
        [TestMethod]
        public void TestUndo()
        {
            var hookedItem = new TestActionHookedItem();
            var revertable = new TestRevertableModel(2, 5.3, hookedItem);

            revertable.Foo = 54;
            ActionHookHistory.Undo();
            revertable.Foo.Should().Be(2);
            ActionHookHistory.Redo();
            revertable.Foo.Should().Be(54);
            
            revertable.Bar = 1.5;
            ActionHookHistory.Undo();
            revertable.Bar.Should().Be(5.3);
            ActionHookHistory.Redo();
            revertable.Bar.Should().Be(1.5);

            revertable.FooBar.Add(5);
            revertable.FooBar.Add(8);
            revertable.FooBar.Add(2);
            ActionHookHistory.Undo();
            revertable.FooBar.Values.Should().BeEquivalentTo(5, 8);
            revertable.FooBar.Clear();
            revertable.FooBar.Add(4);
            revertable.FooBar.Add(10);
            revertable.FooBar.Add(6);
            revertable.FooBar.Values.Should().BeEquivalentTo(4, 10, 6);
            ActionHookHistory.Undo();
            ActionHookHistory.Undo();
            ActionHookHistory.Undo();
            ActionHookHistory.Undo();
            revertable.FooBar.Values.Should().BeEquivalentTo(5, 8);
            ActionHookHistory.Redo();
            ActionHookHistory.Redo();
            ActionHookHistory.Redo();
            ActionHookHistory.Redo();
            revertable.FooBar.RemoveAt(1);
            ActionHookHistory.Undo();
            revertable.FooBar.Values.Should().BeEquivalentTo(4, 10, 6);
            ActionHookHistory.Redo();
            revertable.FooBar.Values.Should().BeEquivalentTo(4, 6);
            ActionHookHistory.ClearStack();
        }

        [TestMethod]
        public void TestUndoAll()
        {
            var hookedItem = new TestActionHookedItem();
            var revertable = new TestRevertableModel(2, 5.3, hookedItem);


            revertable.Foo = 18;
            hookedItem.Add(49);
            hookedItem.Add(5);
            revertable.Bar = 43.5;
            hookedItem.RemoveAt(1);
            revertable.Foo = 10;
            hookedItem.Clear();
            hookedItem.Add(632);
            revertable.Foo = hookedItem.Values[0];
            hookedItem.Add(34);
            hookedItem.RemoveAt(0);
            ActionHookHistory.Undo();
            ActionHookHistory.Undo();
            revertable.Foo.Should().Be(632);
            ActionHookHistory.Undo();
            revertable.Foo.Should().Be(10);
            ActionHookHistory.Redo();
            revertable.Foo.Should().Be(632);
            ActionHookHistory.Redo();
            ActionHookHistory.Redo();

            ActionHookHistory.UndoAll();
            revertable.Should().BeEquivalentTo(new TestRevertableModel(2, 5.3, hookedItem));
            hookedItem.Values.Should().BeEmpty();
            ActionHookHistory.ClearStack();
        }
    }

    internal class TestActionHookedItem
    {
        private List<int> _values;

        public IReadOnlyList<int> Values => _values;

        public TestActionHookedItem()
        {
            _values = new List<int>();
        }

        public void Clear()
        {
            var copy = new List<int>(_values);

            Action undo = () => _values = copy;
            Action redo = () => _values.Clear();

            ActionHookHistory.PushAction(new BasicActionHook(this, redo, undo));
            redo();
        }

        public void Add(int item)
        {
            Action undo = () => _values.RemoveAt(_values.Count - 1);
            Action redo = () => _values.Add(item);

            ActionHookHistory.PushAction(new BasicActionHook(this, redo, undo));
            redo();
        }

        public void RemoveAt(int index)
        {
            int value = _values[index];
            Action undo = () => _values.Insert(index, value);
            Action redo = () => _values.RemoveAt(index);

            ActionHookHistory.PushAction(new BasicActionHook(this, redo, undo));
            redo();
        }
    }

    internal class TestRevertableModel : RevertableModel
    {
        public int Foo
        {
            get => _foo;
            set
            {
                if (_foo != value)
                {
                    BeforeChange();
                    _foo = value;
                    OnChanged();
                }
            }
        }
        public double Bar
        {
            get => _bar;
            set
            {
                if (_bar != value)
                {
                    BeforeChange();
                    _bar = value;
                    OnChanged();
                }
            }
        }

        public TestActionHookedItem FooBar;
        private int _foo;
        private double _bar;

        public TestRevertableModel(int foo, double bar, TestActionHookedItem fooBar)
        {
            _foo = foo;
            _bar = bar;
            FooBar = fooBar;
        }

        public override RevertableModel Copy()
        {
            return new TestRevertableModel(Foo, Bar, null);
        }

        public override void Reload(RevertableModel state)
        {
            var m = (TestRevertableModel)state;
            _foo = m.Foo;
            _bar = m.Bar;
        }
    }
}
