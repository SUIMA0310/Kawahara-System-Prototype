using System;
using System.Collections;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Reactive.Linq;
using System.Reactive.Subjects;
using System.Text;
using System.Threading.Tasks;

namespace WindowsClientApplication.Models {

    public class Container<T> : IEnumerable<T> {

        private Subject<T> Subject { get; }
        private Queue<T> Queue { get; }

        /// <summary>
        /// アイテムの有効時間をmsで指定
        /// </summary>
        public int RemoveTime { get; set; } = 1000;

        public Func<T, bool> Check { get; set; }

        public Container() {

            this.Queue = new Queue<T>();
            this.Subject = new Subject<T>();
            this.Subject
                .Do( x => {

                    //アイテムを挿入
                    lock ( this.Queue ) {

                        this.Queue.Enqueue( x );

                    }

                } )
                .Throttle( TimeSpan.FromMilliseconds( this.RemoveTime ) )
                .Subscribe( _ => {

                    //有効期限切れにより削除
                    lock ( this.Queue ) {

                        while ( this.Queue.Any() && ( Check?.Invoke( this.Queue.Peek() ) ?? true ) ) {
                            this.Queue.Dequeue();
                            if ( Check == null ) {
                                break;
                            }
                        }

                    }

                } );

        }

        /// <summary>
        /// アイテムを挿入する
        /// </summary>
        public void Insert( T item ) {

            this.Subject.OnNext( item );

        }

        public IEnumerator<T> GetEnumerator() {

            IEnumerable<T> staticData;

            lock ( this.Queue ) {

                staticData = this.Queue.ToArray();

            }

            return staticData.GetEnumerator();

        }
        IEnumerator IEnumerable.GetEnumerator() {

            return this.GetEnumerator();

        }

    }

}
